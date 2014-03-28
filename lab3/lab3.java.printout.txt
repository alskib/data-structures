// Franklin Leung
// Lab 3 - COSC 3319
// Fall 2012

package lab3;

import java.io.BufferedReader;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.Scanner;

public class Lab3 {
    private static final String filePath = "C:/Users/franklin/Documents/My Dropbox/class/cosc3319/labs/lab3/";

    public static void main(String[] args) {
        
        int KN, F, R, K;
        Node p;
        Object J;
        ArrayList storeNumbers = new ArrayList();
        String fileName = "";

        do {
            // Get relations from text file
            System.out.println("Enter input filename or \"EXIT\": ");
            Scanner sc = new Scanner(System.in);
            fileName = sc.next();
            String fullPath = filePath + fileName;
            try {
                BufferedReader in = new BufferedReader (new FileReader(fullPath));
                String fileText = "";
                storeNumbers.clear();
                
                // step 1 - read relations
                while(in.ready()) {
                    storeNumbers.add(in.readLine() + " ");
                }
                in.close();

                int relationCount = storeNumbers.size();
                System.out.println("Processing " + relationCount + " relations.");
                
                for (int i = 0; i < storeNumbers.size(); i++) {
                    fileText += storeNumbers.get(i);
                }
                
                KN = relationCount;
                int[] Count = new int[relationCount + 1];
                int[] QLink = new int[relationCount + 1];
                Object[] Top = new Object[relationCount + 1];
                
                for (int i = 0; i <= relationCount; i++) {
                    Count[i] = 0;
                    Top[i] = null;
                }
                
                // step 2 - assign relations
                Scanner fi = new Scanner(fileText);
                J = 0;
                K = 0;
                while (true) {
                    if (fi.hasNextInt()) {
                        J = fi.nextInt();
                        if (fi.hasNextInt())
                            K = fi.nextInt();
                        System.out.println("Processing " + J + " < " + K);
                        Count[(int)K]++;
                        p = new Node(K);
                        p.setNext((Node)Top[(int)J]);
                        Top[(int)J] = p;
                    }
                    else
                        break;
                }

                // step 3
                R = 0;
                QLink[0] = 0;
                for (int i = 1; i <= relationCount; i++) {
                    if (Count[i] == 0) {
                        QLink[(int)R] = i;
                        R = i;
                    }
                }
                F = (int)QLink[0];
                QLink[relationCount] = 0;
                
                // step 4
                while (F != 0) {
                    System.out.println("Performing action: " + F);
                    
                    KN--;
                    p = (Node)Top[(int)F];
                    Top[(int)F] = null;
                    while (p != null) {
                        Count[(int)p.getAction()]--;
                        if (Count[(int)p.getAction()] == 0) {
                            QLink[(int)R] = (int)p.getAction();
                            R = (int)p.getAction();
                        }
                        p = p.getNext();
                    }
                    F = (int)QLink[F];
                }
                
                
                // step 5
                if (KN == 0) {
                    System.out.println("Process completed.");
                } else {
                    System.out.println("Loop found.");
                    for (int i = 1; i <= relationCount; i++)
                        QLink[i] = 0;
                }
                
                // B option - loop detection
                
                // step 6
                for (int i = 1; i <= relationCount; i++) {
                    p = (Node)Top[i];
                    Top[i] = 0;
                    while (p != null && QLink[(int)p.getAction()] == 0) {
                        QLink[(int)p.getAction()] = i;
                        if (p != null)
                            p = p.getNext();
                    }
                }
                
                // step 7
                K = 1;
                while ((int)QLink[K] == 0)
                    K++;
                
                // step 8
                do {
                    Top[K] = 1;
                    K = (int)QLink[K];
                } while ((int)Top[K] == 0);
                
                // step 9
                while ((int)Top[K] != 0) {
                    System.out.println(K + "...");
                    Top[K] = 0;
                    K = QLink[K];
                }
                
                
            } catch (Exception e) {
                System.out.println(e.getMessage());
            }
        } while (!fileName.equals("EXIT"));
        System.out.println("Terminating program.");
    }
}