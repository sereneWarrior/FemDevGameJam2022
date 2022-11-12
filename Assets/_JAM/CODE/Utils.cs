using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net;
//using System.IO;
public class Utils : MonoBehaviour
{
	public static Vector2 getScreenSize()
	{
		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		Vector2 size = new Vector2(worldScreenWidth, worldScreenHeight);

		return size;
	}

	public static Vector3 getTouchPos(Vector3 coords)
	{
		float cameraZPos = -Camera.main.transform.position.z;
		Vector3 tempCoords = new Vector3(coords.x, coords.y, cameraZPos);
		Vector3 worlPoint = Camera.main.ScreenToWorldPoint(tempCoords);
		Vector2 touchPos = new Vector3(worlPoint.x, worlPoint.y);
		return touchPos;
	}

	public static float getPercent(float total, float part)
	{
		return (100 / total) * part;
	}

	public static float getSizeFromPercent(float percent, float total)
	{
		return (total / 100) * percent;
	}

	public static Color32 HexToColor(string hex)
	{
		var r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		var g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		var b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}

	public static float ConvertRange(
		float originalStart, float originalEnd, // original range
		float newStart, float newEnd, // desired range
		float value) // value to convert
	{
		float scale = (float)(newEnd - newStart) / (originalEnd - originalStart);
		return (float)(newStart + ((value - originalStart) * scale));
	}

	public static char GenerateRandomChar()
    {
		string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		char c = st[UnityEngine.Random.Range(0, st.Length)];
		return c;
    }

	public static string CreateRandomSequence(RoomCodeType roomCodeType = RoomCodeType.Letters, int length = 6)
    {
		string randomSequence = "";
		int letterOrNumber = 0;

		for (int i = 0; i < length; i++)
        {
            if (roomCodeType == RoomCodeType.Numbers)
            {
				randomSequence += UnityEngine.Random.Range(0, 10).ToString();
            }
			else if (roomCodeType == RoomCodeType.Letters)
            {
				randomSequence += GenerateRandomChar();
			}
            else if(roomCodeType == RoomCodeType.LettersAndNumbers)
            {
				letterOrNumber = UnityEngine.Random.Range(0, 100);
				if (letterOrNumber < 25 || letterOrNumber > 75)
					randomSequence += UnityEngine.Random.Range(0, 10).ToString();
				else
					randomSequence += GenerateRandomChar();
			}
        }

		return randomSequence;
    }

	/*public static bool checkInternetConnection() {
		bool isConnected = false;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://google.com");
		try {
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess) {
					using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {

						isConnected = true;
					}
				}
			}
		} catch {
			isConnected = false;
		}
		return isConnected;
	}*/
}

public static class EnumerableExtension
{
	public static IEnumerable<IEnumerable<T>> Split<T>(this ICollection<T> self, int chunkSize)
	{
		var splitList = new List<List<T>>();
		var chunkCount = (int)Math.Ceiling((double)self.Count / (double)chunkSize);

		for (int c = 0; c < chunkCount; c++)
		{
			var skip = c * chunkSize;
			var take = skip + chunkSize;
			var chunk = new List<T>(chunkSize);

			for (int e = skip; e < take && e < self.Count; e++)
			{
				chunk.Add(self.ElementAt(e));
			}

			splitList.Add(chunk);
		}

		return splitList;
	}
}

public enum RoomCodeType
{
	Letters,
	Numbers,
	LettersAndNumbers,
}