using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEditor.Sprites;

class Line
{
	Vector2 pointA;
	Vector2 pointB;

	public Line(Vector2 initA, Vector2 initB)
	{
		pointA = initA;
		pointB = initB;
	}
}

public class WirePuzzleController : MonoBehaviour {
	Sprite s;

	List<Line> puzzlePlacementSolutionLines;

	// Use this for initialization
	void Start () {
		Debug.Log("hey start");
		Load("Assets/WirePuzzleStuff/WirePuzzle3.txt");
	}
	
	// Update is called once per frame
	void Update () {

	}

	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			puzzlePlacementSolutionLines = new List<Line>();
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');
						if (entries.Length > 0)
						{
							Vector2 a = new Vector2(float.Parse(entries[0]), float.Parse(entries[1]));
							Vector2 b = new Vector2(float.Parse(entries[2]), float.Parse(entries[3]));
							puzzlePlacementSolutionLines.Add(new Line(a, b));
						}
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (IOException e)
		{
			Debug.Log(e);
			return false;
		}
	}
}
