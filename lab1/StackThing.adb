WITH Ada.Text_IO; USE Ada.Text_IO;

PACKAGE BODY StackThing IS
   S: ARRAY(1..Max_Size) OF Item;
   Top: Integer RANGE 0..Max_Size := 0; --initiate to zero

   PROCEDURE Push(X: IN Item; Z: OUT Boolean) IS
   BEGIN
      IF Top < Max_Size THEN
         Top := Top + 1;
         S(Top) := X;
         Z := True;
      ELSE
         Z := False;
         Put_Line("Error: Overflow");
      END IF;
   END Push;

   PROCEDURE Pop(X: OUT Item; Z: OUT Boolean) IS
   BEGIN
      IF Top = 0 THEN
         Z := False;
         Put_Line("Error: Underflow");
      ELSE
         Z := True;
         X := S(Top);
         Top := Top - 1;
      END IF;
   END Pop;
END StackThing;


