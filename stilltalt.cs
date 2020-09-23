using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    const double g = 9.81;
    static List<Driver> drivers = new List<Driver>();
    static List<int> bends = new List<int>();
    static int maxSpeed = int.MaxValue;

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int v = int.Parse(Console.ReadLine());
        char ranking = 'a';

        for (int i = 0; i < n; i++)
        {
            int speed = int.Parse(Console.ReadLine());
            drivers.Add(new Driver(ranking,speed));
            ranking++;
        }
        for (int i = 0; i < v; i++)
        {
            int bend = int.Parse(Console.ReadLine());
            bends.Add(bend);
        }

        drivers = sortSpeed(drivers);
        int max = optimalSpeed();
        List<Driver> res = FallOrNot(drivers);

        Console.WriteLine(max);
        Console.WriteLine("y");
        foreach(Driver d in drivers){
            Console.WriteLine(d.Ranking + " " + d.Speed);
        }
    }

    static int optimalSpeed(){
        foreach(int i in bends){
            int speed = (int)Math.Sqrt(Math.Tan(ConvertToRadians(60)) * (i * g));
            if(speed < maxSpeed){
                maxSpeed = speed;
            }
        }
        return maxSpeed;
    }

    static List<Driver> sortSpeed(List<Driver> driversSort) {

        int size = driversSort.Count;
        for ( int i = 0; i < size-1; i++){
            for ( int j = 0; j<size-i-1; j++) {
                if (driversSort[j].Speed < driversSort[j+1].Speed)
                {
                    Driver temp = driversSort[j];
                    driversSort[j] = driversSort[j+1];
                    driversSort[j+1] = temp;
                }
            }
        }
        return driversSort;
    }

    static List<Driver> FallOrNot(List<Driver> drivers) {
        IEnumerator<Driver> temp = drivers.GetEnumerator();
        List<Driver> result = new List<Driver>();
        bool hasNext = temp.MoveNext();
        foreach(int b in bends){
            while(hasNext){
                temp = drivers.GetEnumerator();
                Driver d = temp.Current;

                if(ConvertToDegrees(Math.Atan(d.Speed * d.Speed / (b * g))) > 60){
                        result.Add(d);
                        drivers.Remove(d);
                    }
            }
        }

        result.AddRange(drivers);
        return result;

        
            /*for(int i = 0; i<drivers.Count;i++){
                if(ConvertToDegrees(Math.Atan(drivers[i].Speed * drivers[i].Speed / (i * g))) > 60){
                    result.Add(temp[i]);
                    temp.Remove(temp[i]);
                }
            }
        
        return result;*/
     }

    static double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    static double ConvertToDegrees(double radians)
    {
        return (180 / Math.PI) * radians;
    }
}

class Driver
{
    private char ranking;
    private int speed;

    public Driver(char ranking, int speed){
        this.Speed = speed;
        this.Ranking = ranking;
    }
    

    public int Speed {
        get => this.speed;
        set => this.speed = value;
    }

    public char Ranking {
        get => this.ranking;
        set => this.ranking = value;
    }

}
