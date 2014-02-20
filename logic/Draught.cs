﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Draught : Token
    {
        public Draught() { tok = "draugth"; }
        public Draught(PlayerColor c) : base(c) { tok = "draught"; }
        /*Valid moves diagonalTop Left and Right
         *List<int> a is a List with all yet  found moves
         *pos0 is the x-coordinate of draught
         *pos1 is the y-coordinate of draught
         *field is the playground
         *j is a running variable
         */
        public List<int> diagonalTop(List<int> a, int pos0, int pos1, Map field, int j)
        {
            int length = field.Field.GetLength(1);
            List<int> ret = a;
            PlayerColor pc = PlayerColor.White;
            if (field.Field[pos0, pos1] != null && field.Field[pos0, pos1].Color == pc )
                pc = PlayerColor.Black;
            if (pos0 -Math.Abs(j) >= 0 && ((j > 0 && pos1 + Math.Abs(j) < length ) || (j < 0 && pos1 + j >= 0)))
            {
                if (field.Field[pos0 - Math.Abs(j), pos1 + j] == null)// is empty
                {
                    ret.Add(0);    //you can move to this field
                    return diagonalTop(ret, pos0, pos1, field, (Math.Abs(j)/j)*((Math.Abs(j) + 1)));
                }
                else if (field.Field[pos0 - Math.Abs(j), pos1 + Math.Abs(j)].Color == pc)
                {
                    if (pos0 - j - 1 >= 0 && pos1 + j + 1 < length)
                    {
                        var temp = field.Field[pos0 - j - 1, pos1 + j + 1];
                        if (j < 0)
                            temp = field.Field[pos0 + j - 1, pos1 + j - 1];
                        if (temp == null)
                        {
                            ret.Add(-1);
                            ret.Add(1);// You can move to this field with priority 
                            return diagonalTop(ret, pos0, pos1, field, ((Math.Abs(j)/j) * (Math.Abs(j) + 1)));
                        }
                    }
                }
                else
                {
                    ret.Add(-1);
                    return diagonalTop(ret, pos0, pos1, field, ((Math.Abs(j)/j) * (Math.Abs(j) + 1)));
                }

            }
            else
            {
                if (field.Field[pos0 - Math.Abs(j), pos1 + j] == null)// is empty
                {
                    ret.Add(0);    //you can move to this field
                }
                else if (field.Field[pos0 - Math.Abs(j), pos1 + j].Color == pc)
                {
                    if (pos0 - Math.Abs(j) - 1 >= 0 && ((j > 0 && pos1 + j + 1 < length) || (j < 0 && pos1 + j - 1 >= 0)))
                    {
                        var temp = field.Field[pos0 - j - 1, pos1 + j + 1];
                        if (j < 0)
                            temp = field.Field[pos0 + j - 1, pos1 + j - 1];
                        if (temp == null)
                        {
                            ret.Add(-1);
                            ret.Add(1);// You can move to this field with priority 
                        }
                    }
                }
                else
                {
                    ret.Add(-1);
                }
            }
            return ret;
        }
        /*Valid moves diagonalBottom Left and Right
        *List<int> a is a List with all yet  found moves
        *pos0 is the x-coordinate of draught
        *pos1 is the y-coordinate of draught
        *field is the playground
        *j is a running variable
        */
        public List<int> diagonalBottom(List<int> a, int pos0, int pos1, Map field, int j)
        {
            int length = field.Field.GetLength(1);
            List<int> ret = a;
            PlayerColor pc = PlayerColor.White;
            if (field.Field[pos0, pos1] != null  && field.Field[pos0, pos1].Color == pc)
                pc = PlayerColor.Black;
            if (pos0 + Math.Abs(j) < length - 1 && ((j > 0 && pos1 + j < length - 1) || (j < 0 && pos1 + j >= 1)))
            {
                if (field.Field[pos0 + j, pos1 + j] == null)// is empty
                {
                    ret.Add(0);    //you can move to this field
                    return diagonalBottom(ret, pos0, pos1, field, Math.Abs(j) + 1);
                }
                else if (field.Field[pos0 + j, pos1 + j].Color == pc)
                {
                    if (pos0 + j + 1 < length && ((j > 0 && pos1 + j + 1 < length) || (j < 0 && pos1 + j - 1 >= 0)))
                    {
                        var temp = field.Field[pos0 + Math.Abs(j) + 1, pos1 + j + 1];
                        if (j < 0)
                            temp = field.Field[pos0 + Math.Abs(j) + 1, pos1 + j - 1];
                        if (temp == null)
                        {
                            ret.Add(-1);
                            ret.Add(1);// You can move to this field with priority 
                            return diagonalBottom(ret, pos0, pos1, field, Math.Abs(j) + 1);
                        }
                    }
                }
                else
                {
                    ret.Add(-1);
                    return diagonalBottom(ret, pos0, pos1, field, Math.Abs(j) + 1);
                }

            }
            else
            {
                if (field.Field[pos0 + j, pos1 + j] == null)// is empty
                {
                    ret.Add(0);    //you can move to this field
                }
                else if (field.Field[pos0 + j, pos1 + j].Color == PlayerColor.White)
                {
                    if (pos0 + j + 1 < length && pos1 + j + 1 < length)
                    {
                        var temp = field.Field[pos0 + Math.Abs(j) + 1, pos1 + j + 1];
                        if (j < 0)
                            temp = field.Field[pos0 + Math.Abs(j) + 1, pos1 + j - 1];
                        if (field.Field[pos0 + j + 1, pos1 + j + 1] == null)
                        {
                            ret.Add(-1);
                            ret.Add(1);// You can move to this field with priority 
                        }
                    }
                }
                else
                {
                    ret.Add(-1);
                }
            }
            return ret;
        }

        public override int[,] nextStep(Map field, int[] position)
        {
            List<int> t = new List<int>();
            List<int> s = new List<int>();
            int k = 1;
            int pos0 = position[0];     // x-coordinate of Draught
            int pos1 = position[1];     // y-coordinate of Draught
            int[,] map = new int[field.Field.GetLength(1), field.Field.GetLength(1)]; // integer-field to return with all viable moves

            for(int a=0;a<map.GetLength(1);++a)      // -1 for a field, where you cannot move forward to
            {
                for (int b=0;b<map.GetLength(1);++b)
                {
                    map[a, b] = -1;
                }
            }
            for (int i = 0; i < 4; ++i)// look in all diagonal directions from this draught where you can go
            {
                switch (i)
                {
                    case 0://diagonal up-left
                        {
                            s = diagonalTop(t, pos0, pos1, field, -k);
                            for (int j = 1; j < s.Count; ++j)
                            {
                                if(pos0 - j >= 0 && pos1 - j >= 0)
                                map[pos0 - j, pos1 - j] = s[j];
                            }
                        }
                        break;
                    case 1://diagonal up-right
                        {
                            s = diagonalTop(t, pos0, pos1, field, k);
                            for (int j = 1; j < s.Count; ++j)
                            {
                                if(pos0 - j >= 0 && pos1 + j < map.GetLength(1))
                                map[pos0 - j, pos1 + j] = s[j];
                            }
                            break;
                        }
                    case 2://diagonal bottom-right
                        {
                            s = diagonalBottom(t, pos0, pos1, field, k);
                            for (int j = 1; j < s.Count; ++j)
                            {
                                if(pos0 + j < map.GetLength(1) && pos1 + j < map.GetLength(1))
                                map[pos0 + j, pos1 + j] = s[j];
                            }
                            break;
                        }
                    case 3://diagonal bottom-left
                        {
                            s = diagonalBottom(t, pos0, pos1, field, -k);
                            for (int j = 1; j < s.Count; ++j)
                            {  
                                if(pos0+j < map.GetLength(1) && pos1 - j >= 0)
                                map[pos0 + j, pos1 - j] = s[j];
                            }
                            break;
                        }
                    default: break;
                }
            }
            return map;
        }

        
      /*  public static void Main(String[] args)
        {
            Map m = new Map(10);
            Draught d = new Draught(PlayerColor.White);
            int l = m.Field.GetLength(1);
            int[] pos = new int[] { 3, 2 };
            int[,] erg = d.nextStep(m, pos);
            for (int i = 0 ; i < l ; ++i)
            {
                for ( int j = 0 ; j < l ; ++j)
                {
                    Console.Write(erg[i, j]);
                    Console.Write("   ");
                }
                Console.WriteLine();
            }
        }*/
         
    }
}
