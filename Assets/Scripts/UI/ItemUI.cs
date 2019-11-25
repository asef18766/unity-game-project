using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
	private Animator animator;
	
	// children objects
	public Image image;
	public Text nameText;
	public Text countText;

	// datas
	public Sprite sprite { get; private set; }
	public string itemName { get; private set; }
	public string description { get; private set; }
	public int count { get; private set; }

	void Start()
	{
		animator = GetComponent<Animator>();
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

	public void select()
	{
		animator.SetBool("select", true);
	}

	public void unselect()
	{
		animator.SetBool("select", false);
	}
}