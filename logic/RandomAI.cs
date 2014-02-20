﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class RandomAI : Player
    {
        //returns a random token which is able to move to at least one field
        public int[] ChooseToken(Map m, Token.PlayerColor color)
        {
            List<int[]> tokens = new List<int[]>();
            List<int[]> priorityTokens = new List<int[]>();
            Random r = new Random();
            int[,] compare = new int[m.Field.GetLength(1), m.Field.GetLength(1)];
            for (int a=0;a<compare.GetLength(1);a++){//fills the map with -1 to compare
                for (int b=0;b<compare.GetLength(1);b++){
                    compare[a, b] = -1;
                }
            }
            for (int i = 0; i < m.Field.GetLength(1); i++) {//build a list off all tokens of a specific color
                for (int j = 0; j < m.Field.GetLength(1); j++){
                    if (m.Field[i, j]!=null&&m.Field[i, j].Color == color)
                    {
                        if (m.getToken(new int[] { i, j }).nextStep(m, new int[] { i, j }) != compare){
                          tokens.Add(new int[] { i, j });
                        }
                        else{
                           ChooseToken(m,color);
                        }
                    }
                }
            }

            int count = 0;
            foreach (var n in tokens){ //checks if there are any priority turns
                int[,]help=m.Field[n[0],n[1]].nextStep(m,new int[]{n[0],n[1]});
                for (int a=0;a<help.GetLength(1);++a){
                    for (int b=0;b<help.GetLength(1);++b){
                        if (help[a, b] == 1){
                            count++;
                            priorityTokens.Add(new int[]{a,b});
                        }
                    }
                }
            }
            if (count == 0){//there are no priority turns
                int r1 = r.Next(0, tokens.Count);
                return new int[] { tokens[r1][0], tokens[r1][1] };
            }
            else{//there are priority turns
                int r1 = r.Next(0, priorityTokens.Count);
                return new int[] { tokens[r1][0], tokens[r1][1] };
            }
        }

        //returns the field the Player wants to visit
        public int[] SetStep(Map m, Token.PlayerColor color, int[] pos)
        {
            int[,] help;
            Random r = new Random();
            int r1;
            List<int[]> priority = new List<int[]>();
            List<int[]> visitable = new List<int[]>();

            if (m.Field[pos[0], pos[1]].Tok == "stone") {
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
               help = new Stone(c).nextStep(m, pos);//field of information where you can move, or have to move a stone
            }
            else{
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
                help = new Draught(c).nextStep(m, pos);//field of information where you can move, or have to move a draught
            }
           
            for (int a=0;a<help.GetLength(1);++a){
                for (int b = 0; b < help.GetLength(1);++b)
                {
                    if (help[a, b] == 1) priority.Add(new int[] { a, b }); //builds a list of field with priority
                    if (help[a, b] == 0) visitable.Add(new int[] { a, b });//builds a list of field which are visitable
                }
            }
            if (priority.Count != 0){//if there are fields with higher priority
                r1 = r.Next(0,priority.Count);
                return new int[] { priority[r1][0],priority[r1][1] };//choose a random field
            }
            else{
                    r1 = r.Next(0,visitable.Count);
                    return new int[] { visitable[r1][0], visitable[r1][1] };
                }
            }

    }
    }

