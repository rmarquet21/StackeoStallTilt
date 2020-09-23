import java.util.*;
import java.io.*;
import java.math.*;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution {

    //Unité d'accélération
    static double g = 9.81;
    //Liste des pilotes
    static List<Driver> drivers = new ArrayList<Driver>();
    //Nombre de virages
    static List<Integer> bends = new ArrayList<Integer>();
    //Vitesse optimal
    static int minSpeed = Integer.MAX_VALUE;
    //Rang pour le classement final
    static char ranking = 'a';

    
    
    public static void main(String args[]) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int v = in.nextInt();
        
        for (int i = 0; i < n; i++) {
            int speed = in.nextInt();
            drivers.add(new Driver(ranking,speed));
            ranking++;
        }
        for (int i = 0; i < v; i++) {
            int bend = in.nextInt();
            bends.add(bend);
        }

        drivers = sortSpeed(drivers);
        drivers = calcResult(drivers);
        int max = optimalSpeed();

        System.out.println(max);
        System.out.println("y");
        drivers.forEach(d -> System.out.println(d.Ranking()));
    }

    //Tri des joueurs avec l'algorithme tri à bulle
    static List<Driver> sortSpeed(List<Driver> driversSort){
        
        int size = driversSort.size();
        for ( int i = 0; i < size-1; i++){
            for ( int j = 0; j<size-i-1; j++) {
                if (driversSort.get(j).Speed() < driversSort.get(j+1).Speed())
                {
                    Driver temp = driversSort.get(j);
                    driversSort.set(j,driversSort.get(j+1));
                    driversSort.set(j+1,temp);
                }
            }
        }
        return driversSort;
    }
    
    //Vitesse minimum ou l'on peut passer tout les virages
    static int optimalSpeed() {
        for (int i : bends) {
            minSpeed = Math.min(minSpeed, (int)(Math.sqrt(Math.tan(Math.toRadians(60)) * i * g)));
        }
        return minSpeed;
    }

    //Calcul du résultats final avec les chutes
     static List<Driver> calcResult(List<Driver> driversRes) {
        List<Driver> result = new ArrayList<>();
        for (int i : bends) {

            int countFall = 0;

            Iterator<Driver> iter = driversRes.iterator();
            while (iter.hasNext()) {
                Driver d = iter.next();

                if (Math.toDegrees(Math.atan(d.Speed() * d.Speed() / (i * g))) > 60) {
                    result.add(countFall++,d);
                    iter.remove();
                }
            }
        }

        result.addAll(0, drivers);
        return result;
    }
}


/**
 * Création d'une classe pour les pilotes
 **/
class Driver {
    private char ranking;
    private int speed;

    public Driver(char ranking, int speed){
        this.ranking = ranking;
        this.speed = speed;
    }
    
    public int Speed(){
        return this.speed;
    }
    
    public char Ranking() {
        return this.ranking;
    }
}