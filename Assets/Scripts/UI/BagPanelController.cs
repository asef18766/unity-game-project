// TODO: rewrite with state machine
// TODO: try UI elements
// TODO: freeze game scene when open panel
using UnityEngine;
using UnityEngine.UI;

public class BagPanelController : MonoBehaviour
{
	private enum BagUIState
	{
		VIEW_ITEM,
		VIEW_INFO
	}

	// current ui state
	private BagUIState state;
	// player instance
	private Player player;

	#region ItemPanel variables
	[Tooltip("Max size for one page of item list")]
	public int maxPageSize;
	// current cursor index in page [0, pageSize)
	private int cursorIndex;
	// current page index
	private int pageIndex;
	// current page size
	private int pageSize;
	// current items on page
	private ItemUI[] itemUIs;
	// whether there exist next page
	private bool hasNextPage;
	private int bagSize;
	private LayoutItemUI layoutItemUI;
	#endregion

	#region InfoPanel variables
	private const int BUTTON_COUNT = 2;
	private int buttonIndex;
	public GameObject useButton;
	public GameObject dropButton;
	private GameObject[] buttons;
	private Image itemImage;
	private Text descriptionText;
	#endregion

	void Start()
	{
		layoutItemUI = transform.Find("ItemList").GetComponent<LayoutItemUI>();

		// find buttons
		useButton = transform.Find("Info/Buttons/ButtonUse").gameObject;
		dropButton = transform.Find("Info/Buttons/ButtonDrop").gameObject;
		buttons = new GameObject[]{ useButton, dropButton };
		fillButton(Color.gray);

		// init values
		state = BagUIState.VIEW_ITEM;
		hasNextPage = false;
		pageIndex = 0;
		cursorIndex = 0;
		itemUIs = layoutItemUI.place(maxPageSize);
		bagSize = Bag.bag_ins.GetBagSize();
		buttonIndex = 0;

		// find ui elements
		itemImage = GameObject.Find("Info/Image").GetComponent<Image>();
		descriptionText = GameObject.Find("Info/Description").GetComponent<Text>();

		// redraw
		redrawItemList();
		redrawInfoPanel();
	}

	void Update()
	{
		// Listen to key event
		switch(state)
		{
			case BagUIState.VIEW_ITEM:
				itemPanelHandle();
				break;
			case BagUIState.VIEW_INFO:
				infoPanelHandle();
				break;
		}
	}

	public void setupPlayer(Player pl)
	{
		player = pl;
	}

	private ItemUI currentItem
	{
		get
		{
			return itemUIs[cursorIndex];
		}
	}

	// item index in bag
	private int itemIndex
	{
		get
		{
			return cursorIndex + (pageIndex - 1) * maxPageSize;
		}
	}

	// fill button with color
	private void fillButton(Color color)
	{
		foreach(GameObject btn in buttons)
			btn.GetComponent<Image>().color = color;
	}

	#region Redraw functions
	private void redrawItemList()
	{
		// calculate page size
		int end = maxPageSize * (pageIndex + 1);
		if(end < bagSize)
		{
			pageSize = maxPageSize;
			hasNextPage = true;
		}
		else
		{
			pageSize = maxPageSize - (end - bagSize);
			hasNextPage = false;
		}

		int start = maxPageSize * pageIndex;
		for(int i = 0; i < pageSize; i++)
		{
			itemUIs[i].gameObject.SetActive(true);
			itemUIs[i].updateInfo(
				Bag.bag_ins.GetItemSprite(start + i),
				Bag.bag_ins.GetItemName(start + i),
				Bag.bag_ins.GetItemDescription(start + i),
				Bag.bag_ins.GetItemCount(start + i)
			);
			itemUIs[i].redraw();
		}

		for(int i = pageSize; i < maxPageSize; i++)
		{
			itemUIs[i].gameObject.SetActive(false);
		}
	}

	private void redrawInfoPanel()
	{
		itemImage.sprite = currentItem.sprite;
		descriptionText.text = currentItem.description;
	}
	#endregion

	#region Handler
	private void itemPanelHandle()
	{
		// update cursor index
		// and check bound (turn page)

		currentItem.unselect();

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			cursorIndex--;
			if(cursorIndex < 0 && pageIndex > 0)
			{
				pageIndex--;
				cursorIndex = maxPageSize - 1;
				redrawItemList();
			}
			cursorIndex = Mathf.Max(0, cursorIndex);
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			cursorIndex++;
			if(hasNextPage && cursorIndex >= pageSize)
			{
				pageIndex++;
				cursorIndex = 0;
				redrawItemList();
			}
			else if(cursorIndex >= pageSize)
			{
				cursorIndex = pageSize - 1;
			}
		}
		// TODO: config for key
		// enter view info state?
		else if(Input.GetKeyDown(KeyCode.Z))
		{
			// set use button as default
			buttonIndex = 0;
			fillButton(Color.white);
			state = BagUIState.VIEW_INFO;
		}
		// exit page
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			player.gameObject.SetActive(true);
			Destroy(gameObject);
		}

		currentItem.select();
		redrawInfoPanel();
	}

	private void infoPanelHandle()
	{
		// use left and right arrow key to select button
		// z to invoke action
		// esc to escape

		// move
		buttons[buttonIndex].GetComponent<Image>().color = Color.white;
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			buttonIndex--;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			buttonIndex++;
		}
		buttonIndex = (buttonIndex + BUTTON_COUNT) % BUTTON_COUNT;
		buttons[buttonIndex].GetComponent<Image>().color = Color.red;

		// select
		if(Input.GetKeyDown(KeyCode.Z))
		{
			switch(buttonIndex)
			{
				// use
				case 0:
					if(Bag.bag_ins.CheckItemEvent(itemIndex, ItemEvent.Use))
					{
						Bag.bag_ins.ExecuteItemEvent(itemIndex, ItemEvent.Use);
					}
					break;
				// drop
				case 1:
					if(Bag.bag_ins.CheckItemEvent(itemIndex, ItemEvent.Use))
					{
						Bag.bag_ins.ExecuteItemEvent(itemIndex, ItemEvent.Drop);
					}
					break;
			}
		}
		// exit
		else if(Input.GetKeyDown(KeyCode.Escape))
		{
			fillButton(Color.gray);
			state = BagUIState.VIEW_ITEM;
		}
	}
	#endregion
}