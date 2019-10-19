using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketTest : MonoBehaviour
{
	public Market market;
	public Text productNameText;
	public Text productPriceText;
	public Button nextProduct;
	public Button prevProduct;
	public Button updateMarket;
	public Button savePriceInfo;

	private List<string> nameList;
	private int productCount;
	private int currIndex;

	void Start()
	{
		this.nameList = this.market.info.price.Keys.ToList();
		this.productCount = this.nameList.Count;
		this.currIndex = 0;

		this.nextProduct.onClick.AddListener(() => {
			this.currIndex++;
			this.currIndex %= this.productCount;
		});

		this.prevProduct.onClick.AddListener(() => {
			this.currIndex--;
			this.currIndex = (this.currIndex + this.productCount) % this.productCount;
		});

		this.updateMarket.onClick.AddListener(() => {
			this.market.updateMarket();
		});
	}

	void Update()
	{
		string name = this.nameList[this.currIndex];
		this.updateProductInfo(name, this.market.info.price[name]);	
	}

	private void updateProductInfo(string name, int price)
	{
		this.productNameText.text = "Name: " + name;
		this.productPriceText.text = "$: " + price;
	}
}