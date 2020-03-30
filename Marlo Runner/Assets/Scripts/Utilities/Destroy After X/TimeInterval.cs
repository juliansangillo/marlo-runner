/** \file TimeInterval.cs
*/

/**
* An enumeration of seconds, minutes, and hours. Since enumerations are fundamentally integers, you 
* can multiply some time already in seconds by one of these to convert it to that interval.
*
* Seconds - Default interval                    <br>
* Minutes - Interval representing 60 seconds    <br>
* Hours - Interval representing 3600 seconds    <br>
*
* <h3>Example:</h3>
* {@code
*   float timer = 5 * TimeInterval.Minutes;
*
*   timer -= Time.deltaTime;
*   if(timer <= 0f)
*       Debug.Log("This ran for 5 minutes, not 5 seconds.");
* }
*
* @author   Julian Sangillo
* @version  1.0
*/
public enum TimeInterval { Seconds = 1, Minutes = 60, Hours = 3600 }
