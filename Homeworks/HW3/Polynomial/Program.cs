using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
class Polynomial
{
    private int degree;
    private double[] coefs;
    public Polynomial()
    {
        this.degree = 0;
        this.coefs = new double[1];
        this.coefs[0] = 0;
    }
    public Polynomial(double[] new_coefs)
    {
        this.degree = new_coefs.Length - 1;
        this.coefs = (double[])new_coefs.Clone();
    }
    public int Degree { get; }
    public double[] Coefs
    {
        get { return (double[])this.coefs.Clone(); }
    }

    public string MakeString(int i)
    {
        string str = "";
        if (i == 1.0)
        {
            if (this.coefs[i] == -1.0) { str += "x"; }
            else if (this.coefs[i] == 1.0) { str += "x"; }
            else { str += Math.Abs(coefs[i]) + "x"; }
            ;
        }
        else
        {
            if (this.coefs[i] == -1.0) { str += "x^" + i; }
            else if (this.coefs[i] == 1.0) { str += "x^" + i; }
            else { str += Math.Abs(coefs[i]) + "x^" + i; }
        }
        
        return str;
    }
    public override string ToString()
    {
        string s = "";

        if (this.coefs[0] != 0) { s += this.coefs[0]; }
        for (int i = 1; i < this.coefs.Length; i++)
        {
            if (this.coefs[i] == 0)
            {
                s += " + ";
                continue;

            }
            else if (this.coefs[i] < 0)
            {
                if (this.coefs[i - 1] != 0)
                {
                    s += " - ";
                    s += MakeString(i);
                }
                else { s += MakeString(i); }
            }
            else
            {
                if (this.coefs[i - 1] != 0)
                {
                    s += " + ";
                    s += MakeString(i);
                }
                else { s += MakeString(i); }
            }
        }

        return s.ToString();
    }
    
    public static Polynomial operator + (Polynomial obj1, Polynomial obj2)
    {
        double[] c_obj1 = (double[])obj1.Coefs.Clone();
        double[] c_obj2 = (double[])obj2.Coefs.Clone();
        int d_obj1 = obj1.coefs.Length - 1;
        int d_obj2 = obj2.coefs.Length - 1;
        int min_d = d_obj1 > d_obj2 ? d_obj2 : d_obj1;
        int max_d = d_obj1 < d_obj2 ? d_obj2 : d_obj1;
        double[] c_obj3 = new double[max_d + 1];


        for (int i = 0; i <= min_d; i++)
        {
            c_obj3[i] = c_obj1[i] + c_obj2[i];
        }
        
        for (int i = min_d + 1; i <= max_d; i++)
        {
            if (d_obj1 > d_obj2)
                c_obj3[i] = c_obj1[i];
            else
                c_obj3[i] = c_obj2[i];
        }

        Polynomial obj3 = new Polynomial(c_obj3);
        
        return obj3;

        
    }
    public static Polynomial operator *(Polynomial obj1, double k)
    {
        double[] c_obj1 = (double[])obj1.Coefs.Clone();
        double[] c_obj3 = new double[c_obj1.Length];
        for (int i = 0; i < c_obj1.Length; i++)
        {
            c_obj3[i] = c_obj1[i] * k;
        }
        Polynomial obj3 = new Polynomial(c_obj3);
        return obj3;
    }

    public static Polynomial operator *(double k, Polynomial obj1)
    {
        return obj1*k;
    }

    double Evaluate(double x)
    {
        double res = 0;
        for (int i = 0; i<this.coefs.Length; i++){
            res+=coefs[i]*(Math.Pow(x, i));
        }
        return res;
    }

    public static Polynomial operator *(Polynomial obj1, Polynomial obj2)
    {
        double[] c_obj1 = (double[])obj1.Coefs.Clone();
        double[] c_obj2 = (double[])obj2.Coefs.Clone();
        int d_obj1 = obj1.coefs.Length - 1;
        int d_obj2 = obj2.coefs.Length - 1;
        double[] c_obj3 = new double[d_obj1+d_obj2+1];
        for (int i=0; i<d_obj1+1; i++)
        {
            for (int j=0; j<d_obj2+1; j++)
            {
                c_obj3[i+j] = c_obj1[i]+c_obj2[j]; 
            }
        }
        Polynomial obj3 = new Polynomial(c_obj3);
        return obj3;
    }

}

class Programm
{

    static void Main(string[] args)
    {
        // Console.Write("How many objets do you want to create? ");
        // int a = Convert.ToInt32(Console.ReadLine());
        // for (int j=0; j<a; j++)
        // {
        //     Console.Write("Please, write degree: ");
        //     int len = Convert.ToInt32(Console.ReadLine());
        //     double[] coeffs = new double[len + 1];
        //     for (int i = 0; i < len + 1; i++)
        //     {
        //         Console.Write("Write the coefficient №"+ (i+1) +": ");
        //         coeffs[i] = Convert.ToDouble(Console.ReadLine());
        //     }
        //     Polynomial p = new Polynomial(coeffs);
        //     Console.WriteLine(p);
        // }
        Console.WriteLine("№1");
        Console.Write("Please, write degree: ");
        int len1 = Convert.ToInt32(Console.ReadLine());
        double[] coeffs1 = new double[len1 + 1];
        for (int i = 0; i < len1 + 1; i++)
        {
            Console.Write("Write the coefficient №"+ (i+1) +": ");
            coeffs1[i] = Convert.ToDouble(Console.ReadLine());
        }
        Polynomial p1 = new Polynomial(coeffs1);
        Console.WriteLine(p1);

        Console.WriteLine("№2");
        Console.Write("Please, write degree: ");
        int len2 = Convert.ToInt32(Console.ReadLine());
        double[] coeffs2 = new double[len2 + 1];
        for (int i = 0; i < len2 + 1; i++)
        {
            Console.Write("Write the coefficient №"+ (i+1) +": ");
            coeffs2[i] = Convert.ToDouble(Console.ReadLine());
        }
        Polynomial p2 = new Polynomial(coeffs2);
        Console.WriteLine(p2);

        Console.WriteLine("You have 2 polynominals. What would you like to do?");
        Console.WriteLine("1. + \n2. * \n3. Print");
        string answer = Console.ReadLine();

        if (answer == "1") { Console.WriteLine(p1 + p2); }
        else if (answer == "2")
        {
            Console.WriteLine("What would you like to do? \n1. polynomial*number \n2. Number*polynomial \n3. Polynomial*polynomial");
            string ans = Console.ReadLine();
            if (ans == "1"){
                Console.Write("What number do you want to multiply the polynomial №1 by? ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(p1 * x);
                }            
            else if (ans == "2")
            {
                Console.Write("What number do you want to multiply the polynomial №1 by? ");
                double x = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine(p1 * x);
            }
            else if (ans == "3") {Console.WriteLine(p1*p2);}
            else {
                Console.WriteLine("Try again");
                ans = Console.ReadLine();}
            
        }
        else if (answer =="3"){ Console.WriteLine(p1); Console.WriteLine(p2);}
        
        else {
            Console.Write("Please, try again");
            answer = Console.ReadLine();
        }
    }
}