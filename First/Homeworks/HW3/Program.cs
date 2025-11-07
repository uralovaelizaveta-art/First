using System;
using System.IO.Compression;
using System.Net;
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
                continue;

            }
            else if (this.coefs[i] < 0)
            {
                if (this.coefs[i - 1] != 0)
                {
                    s += " - ";
                    s += MakeString(i);
                }
                else {s += MakeString(i);}
            }
            else
            {
                if (this.coefs[i - 1] != 0)
                {
                    s += " + ";
                    s += MakeString(i);
                }
                else {s += MakeString(i);}
            }
        }

        return s.ToString();
    }
}

class Programm
{

    static void Main(string[] args)
    {
        Console.Write("Please, write degree: ");
        int len = Convert.ToInt32(Console.ReadLine());
        double[] coeffs = new double[len + 1];
        for (int i = 0; i < len + 1; i++)
        {
            Console.Write("Write the coefficient №"+ (i+1) +": ");
            coeffs[i] = Convert.ToDouble(Console.ReadLine());
        }
        Polynomial p = new Polynomial(coeffs);
        Console.WriteLine(p);
    }

}