using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Stone : Token
    {
        private string token = "stone";
        public Stone(PlayerColor c) : base(c) { }
        public Stone(){}
        //returns a field of integer which say
        //-1 if field must not be visited
        //0 if a field can be visited
        //1 if a field have to be visited (obligation to capture)
        public override int[,] nextStep(Map field, int[] position){
            PlayerColor pc = field.Field[position[0], position[1]].Color;
            int[,] map = new int[field.Field.Length, field.Field.Length];
            foreach (int a in map){//fills the map with -1
                foreach (int b in map){
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
                        map[field1[0] - v, field1[1] + v] = s;
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
                        map[field2[0] - v, field2[1] - v] = s;
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
                        map[field3[0] + v, field3[1] - v] = s;
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
                        map[field4[0] + v, field4[1] + v] = s;
                    }
                }
            }
            return map;
        }

        //returns -1 if field must not be visited
        //returns 0 if a field can be visited
        //returns 1 if a field have to be visited (obligation to capture)
        private int IsValid(int[] field1, Map field, char direction, PlayerColor p)
        {
            int valid = -1;//default
            PlayerColor pc = field.Field[field1[0],field1[1]].Color;
            if (p == pc)
            {
                pc = PlayerColor.Black;
            }
            if (field1[0] < field.Field.Length && field1[1] < field.Field.Length){//field is on the map
                if (field.Field[field1[0], field1[1]] == null){//field is free
                    return valid=0;
                }
                else if (pc != p) 
                {//if an opponent is on that field 
                    if (direction == 'L')
                    {//field is to the left
                        if (pc == PlayerColor.White)
                        {
                            if (field.Field[field1[0] + 1, field1[1] - 1] == null)
                            {//it is possible to jump over
                                valid = 1;//high priority
                            }
                        }
                        else
                        {
                            if (field.Field[field1[0] - 1, field1[1] - 1] == null)
                            {
                                return valid = 1;//high priority
                            }
                        }
                    }
                    else
                    {//field is to the right
                        if (pc == PlayerColor.White)
                        {
                            if (field.Field[field1[0] + 1, field1[1] + 1] == null)
                            {//it is possible to jump over
                                valid = 1;
                            }
                        }
                        else
                        {
                            if (field.Field[field1[0] - 1, field1[1] + 1] == null)
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
