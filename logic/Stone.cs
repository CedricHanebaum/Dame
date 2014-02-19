using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Stone : Token
    {
     
        public Stone(PlayerColor c) : base(c) { tok = "stone"; }
        public Stone() { tok = "stone"; }
        
        //returns a field of integer which say
        //-1 if field must not be visited
        //0 if a field can be visited
        //1 if a field have to be visited (obligation to capture)
        public override int[,] nextStep(Map field, int[] position){
            PlayerColor pc = field.Field[position[0], position[1]].Color;
            int[,] map = new int[field.Field.GetLength(1), field.Field.GetLength(1)];
            for (int a = 0; a < map.GetLength(1);++a ){//fills the map with -1
                for (int b = 0; b < map.GetLength(1); ++b)
                {
                    map[a, b] = -1;
                }
            }
            int[] field1 = { position[0] + 1, position[1] - 1 };//field down left 
            int[] field2 = { position[0] + 1, position[1] + 1 };//field down rigth
            int[] field3 = { position[0] - 1, position[1] - 1 };//field up left 
            int[] field4 = { position[0] - 1, position[1] + 1 };//field up rigth
            if (pc == PlayerColor.Black)
            {
                int s = IsValid(field1, field, 'L', pc);
                int v = (int)(s * Math.Pow(-1, s) * 2);
                if (s == 0)
                {
                    map[field1[0], field1[1]] = s;
                }
                else
                {
                    if (s == 1)
                    {
                        map[position[0] - v, position[1] + v] = s;
                    }
                }
                s = IsValid(field2, field, 'R', pc);
                v = (int)(s * Math.Pow(-1, s) * 2);
                if (s == 0)
                {
                    map[field2[0], field2[1]] = s;
                }
                else
                {
                    if (s == 1)
                    {
                        map[position[0] - v, position[1] - v] = s;
                    }
                }
            }
            else
            {
                int s = IsValid(field3, field, 'L', pc);
                int v = (int)(s * Math.Pow(-1, s) * 2);
                if (s == 0)
                {
                    map[field3[0], field3[1]] = s;
                }
                else
                {
                    if (s == 1)
                    {
                        map[position[0] + v, position[1] + v] = s;
                    }
                }
                s = IsValid(field4, field, 'R', pc);
                v = (int)(s * Math.Pow(-1, s) * 2);
                if (s == 0)
                {
                    map[field4[0], field4[1]] = s;
                }
                else
                {
                    if (s == 1)
                    {
                        if(isOnTheMap(new int[]{position[0] + v, position[1] + v},field)){
                        map[position[0] + v, position[1] - v] = s;
                        }
                    }
                }
            }
            return map;
        }

        public bool isOnTheMap(int[] pos,Map m)
        {
            if (pos[0] >= 0 && pos[0] < m.Field.GetLength(1) && pos[1] >= 0 && pos[1] < m.Field.GetLength(1))
            {
                return true;
            }
            return false;
        }

        //returns -1 if field must not be visited
        //returns 0 if a field can be visited
        //returns 1 if a field have to be visited (obligation to capture)
        private int IsValid(int[] field1, Map field, char direction, PlayerColor p)
        {
            if (!this.isOnTheMap(new int[]{field1[0],field1[1]},field)) return -1;
            int valid = -1;//default
            PlayerColor pc = PlayerColor.White;
            if (field.Field[field1[0], field1[1]] != null)
            {
                pc=field.Field[field1[0], field1[1]].Color;
            }
            if (p == pc)
            {
                pc = PlayerColor.Black;
            }
            if (field1[0]>=0&&field1[0] < field.Field.GetLength(1) && field1[1]>=0&&field1[1] < field.Field.GetLength(1)){//field is on the map
                if (field.Field[field1[0], field1[1]] == null){//field is free
                    return valid=0;
                }
                else if (pc != p) 
                {//if an opponent is on that field 
                    if (direction == 'L')
                    {//field is to the left
                        if (pc == PlayerColor.White)
                        {
                            if (isOnTheMap(new int[] { field1[0] + 1, field1[1] - 1 }, field) && field.Field[field1[0] + 1, field1[1] - 1] == null)
                            {//it is possible to jump over
                                valid = 1;//high priority
                            }
                        }
                        else
                        {
                            if (isOnTheMap(new int[] { field1[0] - 1, field1[1] - 1 }, field) && field.Field[field1[0] - 1, field1[1] - 1] == null)
                            {
                                return valid = 1;//high priority
                            }
                        }
                    }
                    else
                    {//field is to the right
                        if (pc == PlayerColor.White)
                        {
                            if (isOnTheMap(new int[] { field1[0] + 1, field1[1] + 1 }, field) && field.Field[field1[0] + 1, field1[1] + 1] == null)
                            {//it is possible to jump over
                                valid = 1;
                            }
                        }
                        else
                        {
                            if (isOnTheMap(new int[] { field1[0] - 1, field1[1] + 1 }, field) && field.Field[field1[0] - 1, field1[1] + 1] == null)
                            {//it is possible to jump over
                                return valid = 1;
                            }
                        }
                    }
                }
            }
            return valid;
        }
    }
}
