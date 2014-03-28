-- generic stack body

WITH Ada.Text_IO; USE Ada.Text_IO;

PACKAGE BODY StackThing IS
   package I_IO is new Ada.Text_IO.Integer_IO(Integer);
   use I_IO;

   Top		: array(0..N+1) of integer;
   Base		: array(0..N+1) of integer;
   StackSpace	: array(L0..M) of item;

   type nodes is record
      OldTop: integer;
      Growth: integer;
      NewBase: integer;
   end record;

   StackInfo	: array(1..N+1) of nodes;

   overflow	: boolean := false;

   AvailSpace, TotalInc, J : integer;

   Alpha, Beta, Sigma, Tau, Minspace, GrowthAllocate : float;
   EqualAllocate : float := 0.11;

   function floor(f:float) return integer is
      temp : integer;
   begin
      temp := integer(f);
      if (float(temp) <= f) then
         return temp;
      else
         return temp - 1;
      end if;
   end;

   procedure MoveStack is
      aDelta : integer;
      J, L : Integer;
   begin
      for J in 2..N loop
         if StackInfo(J).NewBase < Base(J) then
            aDelta := Base(J) - StackInfo(J).NewBase;
            for L in (Base(J) + 1)..Top(J) loop
               StackSpace(L - aDelta) := StackSpace(L);
            end loop;
            Base(J) := StackInfo(J).NewBase;
            Top(J) := Top(J) - aDelta;
         end if;
      end loop;
      J := N;
      for J in reverse 2..N loop
         if StackInfo(J).NewBase > Base(J) then
            aDelta := StackInfo(J).NewBase - Base(J);
            L := Top(J);
            while L >= (Base(J) + 1) loop
               StackSpace(L + aDelta) := StackSpace(L);
               L := L - 1;
            end loop;
            base(J) := StackInfo(J).NewBase;
            Top(J) := Top(J) + aDelta;
         end if;
      end loop;
   end MoveStack;

   function Reallocate(myItem: item; K: integer) return boolean is
   begin
      AvailSpace := M - L0;
      TotalInc := 0;
      J := N;
      New_Line;
      Put_Line("Contents before repacking: ");
      for J in 1..N+1 loop
         Put("Base["); Put(J, 0); Put("] = "); Put(Base(J),2); Put(". ");
         Put("Top["); Put(J, 0); Put("] = "); Put(Top(J),2); Put(". ");
         Put("OldTop["); Put(J, 0); Put("] = "); Put(StackInfo(J).OldTop,2); Put(".");
         New_Line;
      end loop;
      while J > 0 loop
         AvailSpace := AvailSpace - (Top(J) - Base(J));
         if (Top(J) > StackInfo(J).OldTop) then
            StackInfo(J).Growth := Top(J) - StackInfo(J).OldTop;
            TotalInc := TotalInc + StackInfo(J).Growth;
         else
            StackInfo(J).Growth := 0;
         end if;
         J := J - 1;
      end loop;

      if (float(AvailSpace) < (MinSpace - 1.0)) then
         return false;
      else
         GrowthAllocate := 1.0 - EqualAllocate;
         Alpha := EqualAllocate * float(AvailSpace)/float(N);
         Beta := GrowthAllocate * float(AvailSpace)/float(TotalInc);
         StackInfo(1).NewBase := Base(1);
         Sigma := 0.0;
         for J in 2..N loop
            Tau := Sigma + Alpha + float(StackInfo(J - 1).Growth) * Beta;
            StackInfo(J).NewBase := StackInfo(J - 1).NewBase + (Top(J - 1) - Base(J - 1)) + floor(Tau) - floor(Sigma);
            Sigma := Tau;
         end loop;
         Top(K) := Top(K) - 1;
         MoveStack;
         Top(K) := Top(K) + 1;
         StackSpace(Top(K)) := myItem;
         for J in 1..N loop
            StackInfo(J).OldTop := Top(J);
         end loop;
      end if;
      New_Line;
      Put_Line("Contents after repack: ");
      for J in 1..N loop
         Put("Base["); Put(J, 0); Put("] = "); Put(Base(J), 2); Put(". ");
         Put("Top["); Put(J, 0); Put("] = "); Put(Top(J), 2); Put(".");
         New_Line;
      end loop;
      return true;
   end Reallocate;

   procedure Push(Y: in item; K: in integer; Z: out Boolean) is
   begin
      Top(K) := Top(K) + 1;
      if Top(K) > Base(K + 1) then
         Put_Line("Overflow. Stack full.");
         Z := Reallocate(Y, K);
      else
         StackSpace(Top(K)) := Y;
         z := true;
      end if;
   end Push;

   procedure Pop(Y: out item; K: in integer; Z: out Boolean) is
   begin
      if Top(K) = Base(K) then
         Put_Line("Underflow. Stack empty.");
         Z := false;
      else
         Y := StackSpace(Top(K));
         Top(K) := Top(K) - 1;
         Z := true;
      end if;
   end Pop;

   -- stack initialization
begin
   for K in 1..N+1 loop
      Top(K) := floor((float(K - 1) / float(N) * float(M - L0))) + L0;
      Base(K) := Top(K);
      StackInfo(K).OldTop := Top(K);
   end loop;
   MinSpace := (float(M) - float(L0)) * 0.1;
end StackThing;
