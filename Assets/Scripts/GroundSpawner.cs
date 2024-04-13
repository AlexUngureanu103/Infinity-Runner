using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
	[SerializeField]
	private GameObject _GroundTile;
	[SerializeField]
	private GameObject _GroundTileLeft;
	[SerializeField]
	private GameObject _GroundTileRight;
	[SerializeField]
	private GameObject _TJunctionTile;

	[SerializeField]
	private Rigidbody Player;

	enum SpawnPosition
	{
		None,
		Left,
		Middle,
		Right
	}

	private SpawnPosition lastSpawnPosition;

	private Vector3 nextSpawnPoint = new Vector3(0, 0, 0);
	private Vector3 leftSpawnPoint = new();
	private Vector3 rightSpawnPoint = new();
	private Vector3 leftSpawnOffset = new Vector3(-5, 0, -5);
	private Vector3 rightSpawnOffset = new Vector3(5, 0, -5);

	private GameObject tJunctionTile;
	private List<GameObject> leftTiles = new();
	private List<GameObject> rightTiles = new();

	private bool isJunctionGeneration = false;

	private bool rotateRight = true;

	private bool canRotate = true;
	private float currentYRotation = 0;

	public bool SpawnTile()
	{
		if (nextSpawnPoint.z - Player.transform.position.z > 200 || isJunctionGeneration)
		{
			return false;
		}

		return SpawnTJunction();
	}

	private bool SpawnTJunction()
	{
		isJunctionGeneration = true;
		tJunctionTile = Instantiate(_GroundTile, nextSpawnPoint, Quaternion.Euler(0, currentYRotation, 0));
		nextSpawnPoint = tJunctionTile.transform.GetChild(3).transform.position;

		tJunctionTile = Instantiate(_GroundTile, nextSpawnPoint, Quaternion.Euler(0, currentYRotation, 0));
		nextSpawnPoint = tJunctionTile.transform.GetChild(3).transform.position;

		tJunctionTile = Instantiate(_GroundTile, nextSpawnPoint, Quaternion.Euler(0, currentYRotation, 0));
		nextSpawnPoint = tJunctionTile.transform.GetChild(3).transform.position;

		tJunctionTile = Instantiate(_TJunctionTile, nextSpawnPoint, Quaternion.Euler(0, currentYRotation, 0));
		nextSpawnPoint = tJunctionTile.transform.GetChild(3).transform.position;

		leftSpawnPoint = tJunctionTile.transform.GetChild(7).transform.position;
		rightSpawnPoint = tJunctionTile.transform.GetChild(8).transform.position;

		//Spawn Full Tiles
		var tempLeft = Instantiate(_GroundTile, leftSpawnPoint, Quaternion.Euler(0, currentYRotation - 90, 0));
		leftSpawnPoint = tempLeft.transform.GetChild(3).transform.position;
		leftTiles.Add(tempLeft);

		var tempRight = Instantiate(_GroundTile, rightSpawnPoint, Quaternion.Euler(0, currentYRotation + 90, 0));
		rightSpawnPoint = tempRight.transform.GetChild(3).transform.position;
		rightTiles.Add(tempRight);

		for (int i = 0; i < 8; i++)
		{
			//tempLeft = Instantiate(_GroundTile, leftSpawnPoint, Quaternion.Euler(0, currentYRotation - 90, 0));
			tempLeft = SpawnRandomTile(currentYRotation - 90, leftSpawnPoint);
			leftSpawnPoint = tempLeft.transform.GetChild(3).transform.position;
			leftTiles.Add(tempLeft);

			//tempRight = Instantiate(_GroundTile, rightSpawnPoint, Quaternion.Euler(0, currentYRotation + 90, 0));
			//rightTiles.Add(tempRight);

			tempRight = SpawnRandomTile(currentYRotation + 90, rightSpawnPoint);
			rightSpawnPoint = tempRight.transform.GetChild(3).transform.position;
			rightTiles.Add(tempRight);
		}

		return true;
	}

	private GameObject SpawnRandomTile(float rotation, Vector3 spawnPoint)
	{
		// Generate a random number between 0 and 2
		int randomNumber = Random.Range(0, 3);

		GameObject tileToSpawn;

		switch (randomNumber)
		{
			case 0:
				tileToSpawn = _GroundTile;
				break;
			case 1:
				if (lastSpawnPosition == SpawnPosition.Right)
				{
					lastSpawnPosition = SpawnPosition.Middle;
					tileToSpawn = _GroundTile;
				}
				else
				{
					lastSpawnPosition = SpawnPosition.Left;
					tileToSpawn = _GroundTileLeft;
				}
				break;
			case 2:
				if (lastSpawnPosition == SpawnPosition.Left)
				{
					lastSpawnPosition = SpawnPosition.Middle;
					tileToSpawn = _GroundTile;
				}
				else
				{
					lastSpawnPosition = SpawnPosition.Right;
					tileToSpawn = _GroundTileRight;
				}
				break;
			default:
				tileToSpawn = _GroundTile;
				break;
		}

		var temp = Instantiate(tileToSpawn, spawnPoint, Quaternion.Euler(0, rotation, 0));
		//nextSpawnPoint = temp.transform.GetChild(3).transform.position;

		return temp;
	}

	void Start()
	{
		lastSpawnPosition = SpawnPosition.None;
		for (int i = 0; i < 15; i++)
		{
			SpawnTile();
		}
	}

	void Update()
	{

		if (canRotate)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				rotateRight = false;
				canRotate = false;
				Invoke("ChangePlayerRotation", 1.5f);
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				rotateRight = true;
				canRotate = false;
				Invoke("ChangePlayerRotation", 1.5f);
			}
		}
	}

	public void ChangeTileDirection()
	{

		if (rotateRight)
		{
			currentYRotation += 90;
			nextSpawnPoint = rightSpawnPoint;

			for (int i = 0; i < leftTiles.Count; i++)
			{
				Destroy(leftTiles[i]);
				leftTiles.Remove(leftTiles[i]);
				i--;
			}
			rightTiles.Clear();
		}
		else
		{
			currentYRotation -= 90;
			nextSpawnPoint = leftSpawnPoint;

			for (int i = 0; i < rightTiles.Count; i++)
			{
				Destroy(rightTiles[i]);
				rightTiles.Remove(rightTiles[i]);
				i--;
			}
			leftTiles.Clear();
		}
		isJunctionGeneration = false;
		rotateRight = !rotateRight;
	}

	void ChangePlayerRotation()
	{
		if (currentYRotation == 360 || currentYRotation == -360)
		{
			currentYRotation = 0;
		}
		transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
		canRotate = true;
	}
}
