GENERIC
   Max_Size: Integer := 5;
   TYPE Item IS PRIVATE;

   PACKAGE StackThing IS
      PROCEDURE Push(X: IN Item; Z: OUT Boolean);
      PROCEDURE Pop(X: OUT Item; Z: OUT Boolean);
   END StackThing;
