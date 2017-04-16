/* Bubble Sort : 
	The below source code changes the position of  numbers or changing an unordered sequence into an ordered sequence.
*/

using system;
class bubblesort
{
	static void Main (string args[])
	{
		int[] a = {3,2,5,4,1};
		int t;
		Console.WriteLine("Array: ");
			for (int i =0; i<a.Length; i++)
			{
				Console.WriteLine(a[i]);
			}
			for (int j=0; j<=a.length - 2; j++)
			{
				for (int j=0; j<=a.length - 2; j++)
				{
					if (a[i] > a[i+1])
					{
						t=a[i+1];
						a[i+1] = a[i];
						a[i]=t;						
					}
				}					
			}
			Console.WriteLine("Sorted Array: ");
			foreach (int aray in a)
				Concole.Write(aray+ " ");
			Console.readline();				
				
	}
}

/* output :
Array:
3
2
5
4
1
Sorted Array:
1
2
3
4
5