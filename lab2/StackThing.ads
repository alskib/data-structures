--generic stack specification

GENERIC
   L0 : integer;
   M: integer;
   N: integer;
   TYPE Item IS PRIVATE;

   PACKAGE StackThing IS
      PROCEDURE Push(Y: IN Item; K: in integer; Z: OUT Boolean);
      PROCEDURE Pop(Y: OUT Item; K: in integer; Z: OUT Boolean);
   END StackThing;
