using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float xmin;
	public float xmax;
	public float ymin;
	public float ymax;

	public float con;

	public Transform player;

    private void Start()
    {
		player = GameObject.FindObjectOfType<Player>().transform;

	}

    private void LateUpdate()
	{
		Track();

	}
	private void Track()
	{
		Vector3 posP = player.position;
		Vector3 posC = transform.position;
		
		posP.z = -10;
		
		if (posP.x  > -7.6f)
		{
			if (posP.y > 2.4f + transform.position.y)
			{
				posC.y = Mathf.Clamp(posC.y, ymin, ymax);

			}
			else posP.y = transform.position.y;

			posC.x = Mathf.Clamp(posC.x, xmin, xmax);   //Mathf.Clamp(夾住的值,下限,上限)
			transform.position = Vector3.Lerp(posC, posP, 0.3f * Time.deltaTime * 5);
		}
		
	}
}