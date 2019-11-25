using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
	// children objects
	private Image image;
	private Text nameText;
	private Text countText;

	// datas
	public Sprite sprite { get; private set; }
	public string itemName { get; private set; }
	public string description { get; private set; }
	public int count { get; private set; }

	void Start()
	{
		image = transform.Find("Image").GetComponent<Image>();
		nameText = transform.Find("Name").GetComponent<Text>();
		countText = transform.Find("Count").GetComponent<Text>();
	}

	public void updateInfo(Sprite sp, string iName, string desc, int cnt)
	{
		sprite = sp;
		itemName = iName;
		description = desc;
		count = cnt;
	}

	public void redraw()
	{
		image.sprite = sprite;
		nameText.text = itemName;
		countText.text = "x" + count;
	}
}