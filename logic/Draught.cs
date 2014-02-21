using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Draught : Token
    {
        public Draught() { tok = "draugth"; }
        public Draught(PlayerColor c) : base(c) { tok = "draught"; }
        
        /*
         * Method to find all Valid Moves Diagonal Bottom-left to Top-right
         * pos0 is x-coordinate , pos1 is y-coordinate in Map(position of my stone)
         * field is the playing map
         * j is for calculation ( j can be -1 or 1)
         * in List are integer with value -1(cannot move),0(can move),1 (priority move for jump)
         */

        public List<int> BotleftToTopright(int pos0, int pos1, Map field, int j)
        {
            List<int> ret = new List<int>(); // return list
            int[] pos = new int[2] { pos0, pos1 };//position of your stone
            PlayerColor pc = PlayerColor.White; 
            if (field.Field[pos0, pos1] != null  && field.Field[pos0, pos1].Color == pc)// set opponent Color
                pc = PlayerColor.Black;
            for (int i = 0; i < field.Field.GetLength(1); ++i)
            {
                pos[0] += j * 1;    // Move your stone imaginary to find out, if you can move there
                pos[1] -= j * 1;
                if (field.isOnTheMap(pos))// is the field on the map?
                {
                    if (field.Field[pos[0],pos[1]] == null)// is empty
                    {
                        ret.Add(0);    //you can move to this field
                    }
                    else if (field.Field[pos[0],pos[1]].Color == pc)// is enemystone on the field?
                    {
                        pos[0] += j * 1;
                        pos[1] -= j * 1;
                        if (field.isOnTheMap(pos))  // Can you jump over enemy?
                        {
                            var temp = field.Field[pos[0], pos[1]];
                            if (temp == null)
                            {
                                ret.Add(-1);// Enemy field
                                ret.Add(1);// You can move to this field with priority 
                                i++;// inc i because adding 2 fields
                            }
                        }
                    }
                    else// Your ally stone is on field
                    {//add twice  -1 so that method can see you are not able to move there and behind.
                        ret.Add(-1);
                        ret.Add(-1);
                    }
                }
            }
            for (int c = 0; c < ret.Count - 2; ++c)
            {
                if (ret[c] == ret[c + 1] && ret[c] == -1) // if you cannot move twice in a row 
                {
                    for (int b = ret.Count -1; b > c; --b)// All fields behind will not be accesable
                    {
                        ret.RemoveAt(b);
                        ret.Add(-1);
                    }
                }
            }
            return ret;
        }


        /*
         * Method to find all Valid Moves Diagonal Bottom-right to Top-öeft
         * pos0 is x-coordinate , pos1 is y-coordinate in Map(position of my stone)
         * field is the playing map
         * j is for calculation ( j can be -1 or 1)
         * in List are integer with value -1(cannot move),0(can move),1 (priority move for jump)
         */
        public List<int> BotrightToTopleft(int pos0, int pos1, Map field, int j)
        {
            List<int> ret = new List<int>();// return List
            int[] pos = new int[2] { pos0, pos1 };//Your Position
            PlayerColor pc = PlayerColor.White;
            if (field.Field[pos0, pos1] != null && field.Field[pos0, pos1].Color == pc)//set enemy Color
                pc = PlayerColor.Black;
            for (int i = 0; i < field.Field.GetLength(1); ++i)
            {
                pos[0] += j * 1;// Field in diagonal way
                pos[1] += j * 1;
                if (field.isOnTheMap(pos))// is Field on map?
                {
                    if (field.Field[pos[0], pos[1]] == null)// is empty
                    {
                        ret.Add(0);    //you can move to this field
                    }
                    else if (field.Field[pos[0], pos[1]].Color == pc)// is on requested field enemy?
                    {
                        pos[0] += j * 1;//look behind enemy
                        pos[1] += j * 1;
                        if (field.isOnTheMap(pos))//Can you jump over enemy?
                        {
                            var temp = field.Field[pos[0], pos[1]];
                            if (temp == null)
                            {
                                ret.Add(-1);//field where enemy is standing
                                ret.Add(1);// You can move to this field with priority 
                                i++;
                            }
                        }
                    }
                    else//Ally stone on field
                    {
                        ret.Add(-1);
                        ret.Add(-1);
                    }
                }
            }
            for (int c = 0; c < ret.Count - 2; ++c)
            {
                if (ret[c] == ret[c + 1] && ret[c] == -1)// if you cannot move twice in a row
                {
                    for (int b = ret.Count - 1; b > c; --b)// all fields behind are not accesible
                    {
                        ret.RemoveAt(b);// Removes all entrys till there
                        ret.Add(-1);// add -1 for not reachable
                    }
                }
            }
            return ret;
        }

        /*
         * returns an Array with -1(field where u cannot go),0(field where you can go) and 1(priority move for jump)
         * gets the playing map , and the position of selected draugt
         */
        public override int[,] nextStep(Map field, int[] position)
        {
            List<int> erg0 = new List<int>();
            List<int> erg1 = new List<int>();
            List<int> erg2 = new List<int>();
            List<int> erg3 = new List<int>();
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
                            erg0 = BotrightToTopleft(pos0, pos1, field, -k);
                            for (int j = 1; j <= erg0.Count; ++j)
                            {
                                if(pos0 - j >= 0 && pos1 - j >= 0)
                                map[pos0 - j, pos1 - j] = erg0[j-1];
                            }
                        }
                        break;
                    case 1://diagonal up-right
                        {
                            erg1 = BotleftToTopright(pos0, pos1, field, k);
                            for (int j = 1; j <= erg1.Count; ++j)
                            {
                                if(pos0 + j >= 0 && pos1 - j < map.GetLength(1))
                                map[pos0 + j, pos1 - j] = erg1[j-1];
                            }
                            break;
                        }
                    case 2://diagonal bottom-right
                        {
                            erg2 = BotrightToTopleft(pos0, pos1, field, k);
                            for (int j = 1; j <= erg2.Count; ++j)
                            {
                                if(pos0 + j < map.GetLength(1) && pos1 + j < map.GetLength(1))
                                map[pos0 + j, pos1 + j] = erg2[j-1];
                            }
                            break;
                        }
                    case 3://diagonal bottom-left
                        {
                            erg3 = BotleftToTopright(pos0, pos1, field, -k);
                            for (int j = 1; j <= erg3.Count; ++j)
                            {  
                                if(pos0-j < map.GetLength(1) && pos1 +j >= 0)
                                map[pos0 - j, pos1 + j] = erg3[j-1];
                            }
                            break;
                        }
                    default: break;
                }
            }
            return map;
        }

   //test     
       public static void Main(String[] args)
        {
            Map m = new Map(10);
            Draught d = new Draught(PlayerColor.Black);
            m.Field[3, 2] = new Draught(PlayerColor.Black);
            m.Field[4, 1] = new Stone(PlayerColor.White); 
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
        }
//testend
    }
}
