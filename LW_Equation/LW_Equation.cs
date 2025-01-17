﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW_Equation
{
    public class LinearEquation
    {
        List<float> coefficients;
        public int Size => coefficients.Count;

        /// <summary>
        /// Конструирует уравнение вида aN*x + coefficients[0]y + ... + coefficients[N-2]z + coefficients[N-1] = 0
        /// </summary>
        /// 
        /// Примеры:
        /// <example>
        /// LinearEquation(1,2,3,4) => 1x + 2y + 3z + 4 = 0
        /// LinearEquation(1,2) => 1x + 2 = 0
        /// LinearEquation(1) => 1 = 0 (не имеет решений)
        /// </example>
        /// 
        /// <param name="aN">Последний коэффициент</param>
        /// <param name="coefficients">Остальные коэффициенты</param>
        public LinearEquation(params float[] coefficients)
        {
            this.coefficients = new List<float>();
            this.coefficients.AddRange(coefficients);
        }
        public LinearEquation(List<float> coefficients)
        {
            this.coefficients = new List<float>();
            this.coefficients = coefficients;
        }

        /// <summary>
        /// Суммирует свободный член first с second
        /// </summary>
        static public LinearEquation operator +(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.Size - 1] += second;
            return equation;
        }
        /// <summary>
        /// Вычитает second из свободного члена first
        /// </summary>
        static public LinearEquation operator -(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.Size - 1] -= second;
            return equation;
        }
        public override bool Equals(object obj)
        {
            if (obj is LinearEquation equation)
            {
                if (Size != equation.Size)
                    return false;
                for (int i = 0; i < Size; i++)
                {
                    if (this.coefficients[i] != equation.coefficients[i])
                        return false;
                }

                return true;
            }
            return false;
        }
        static public bool operator ==(LinearEquation first, LinearEquation second)
        {
            return first.Equals(second);
        }
        static public bool operator !=(LinearEquation first, LinearEquation second)
        {
            return !first.Equals(second);
        }
        public float this[int i]
        {
            get 
            { 
                return coefficients[i]; 
            }
            set 
            { 
                coefficients[i] = value; 
            }
        }
        public static LinearEquation operator +(LinearEquation a, LinearEquation b)
        {
            LinearEquation temp = new LinearEquation(a.coefficients);
            for (int i = 0; i < a.Size; i++)
                temp.coefficients[i] = a.coefficients[i] + b.coefficients[i];
            return temp;
        }
        public static LinearEquation operator -(LinearEquation a, LinearEquation b)
        {
            LinearEquation temp = new LinearEquation(a.coefficients);
            for (int i = 0; i < a.Size; i++)
                temp.coefficients[i] = a.coefficients[i] - b.coefficients[i];
            return temp;
        }
        public static implicit operator bool(LinearEquation a)
        {
            if (a.coefficients[a.Size - 1] == 0)
                return true;
            for (int i = 0; i < a.Size - 1; i++)
                if (a.coefficients[i] != 0)
                    return true;

            return false;
        }
        public float? Get(LinearEquation a)
        {
            if (a.Size != 2)
                return null;
            else
                return -a.coefficients[1] / coefficients[0];
        }
        public override string ToString()
        {
            string str = "";
            bool first = true;
            for (int i = 0; i < this.Size - 1; i++)
            {
                if (this.coefficients[i] != 0 && first)
                {
                    if (this.coefficients[i] == 1)
                        str += "x";
                    else if (this.coefficients[i] == -1)
                        str += $"-x";
                    else
                        str += $"{this.coefficients[i]}x";
                    first = false;
                }
                else
                {
                    if (this.coefficients[i] < 0)
                    {
                        if (this.coefficients[i] == -1)
                            str += $"-x";
                        else
                            str += $"{this.coefficients[i]}x";
                    }
                    else if (this.coefficients[i] != 0)
                    {
                        if (this.coefficients[i] == 1)
                            str += $"+x";
                        else
                            str += $"+{this.coefficients[i]}x";
                    }
                }
            }
            if (this.coefficients[this.Size - 1] < 0)
                str += $"{this.coefficients[this.Size - 1]}";
            else if (this.coefficients[this.Size - 1] != 0)
                str += $"+{this.coefficients[this.Size - 1]}";

            str += "=0";
            return str;
        }
        static Random rand = new Random();
        public static LinearEquation RandomLinear(int count)
        {
            List<float> array = new List<float>();
            for (int i = 0; i < count; i++)
                array.Add(rand.Next(-100, 100));
            LinearEquation a = new LinearEquation(array);
            return a;
        }
        public static LinearEquation SpecLinear(int count, float value)
        {
            List<float> ar = new List<float>();
            for (int i = 0; i < count; i++)
                ar.Add(value);
            LinearEquation a = new LinearEquation(ar);
            return a;
        }
        public static LinearEquation operator *(LinearEquation a, float value)
        {
            for (int i = 0; i < a.Size; i++)
            {
                a.coefficients[i] *= value;
                a.coefficients[i] = (float)Math.Round(a.coefficients[i], 2);
            }
            return a;
        }
    }
}
