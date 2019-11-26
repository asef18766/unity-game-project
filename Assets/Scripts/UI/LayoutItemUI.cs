using UnityEngine;

public class LayoutItemUI : MonoBehaviour
{
	public GameObject itemUIObject;
	[Tooltip("initial position")]
	public Vector3 initPos;
	[Tooltip("y delta value")]
	public float deltaY;

	public ItemUI[] place(int count)
	{
		Vector3 current = initPos;
		ItemUI[] ret = new ItemUI[count];

		for(int i=0 ; i<count ; i++)
		{
			GameObject item = Instantiate(itemUIObject, Vector3.zero, Quaternion.identity, transform);
			item.transform.localPosition = current;
			current.y += deltaY;
			ret[i] = item.GetComponent<ItemUI>();
		}

		return ret;
	}
}