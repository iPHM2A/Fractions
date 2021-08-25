using System;
using System.Collections.Generic;
using System.Text;

namespace Fractions
{
    public class Fraction : IComparable<Fraction>, IEquatable<Fraction>
    {
        #region Plus grand déviseur commun [PGCD] 'القاسم المشترك الأكبر'

        public static long pgcd(long a, long b)
        {
            a =Math.Abs(a);
            b = Math.Abs(b);
            long r;
            if (b > a)
            {
                var t = a;
                a = b;
                b = t;
            }

            do
            {
                r = a % b;
                a = b;
                b = r;
            } while (r != 0);

            return a;
        }

        #endregion

        //***

        #region Constructors

        //***
        public Fraction(long a, long b)
        {
            Nominator = (b < 0) ? -a : a;
            if (b == 0)
                throw new ArgumentException($"Denominator cannot be zero. {nameof(b)}");
            denominator = (b < 0) ? -b : b;

        }

        public Fraction()
        {
        }

        //***

        #endregion

        //***

        #region Propriétés

        //***

        #region Nominateur 'البسط'

        //***
        public long Nominator { get; set; }
        //***

        #endregion

        //***

        #region Denominator 'المقام'

        //***
        private long denominator;

        public long Denominator
        {
            get => denominator;
            set
            {
                if (value == 0)
                    throw new ArgumentException($"Denominator cannot be zero. {nameof(denominator)}");
            }
        }

        //***

        #endregion

        //***

        #endregion

        //***

        #region Surcharges

        //***

        #region get positive value

        //***
        public static Fraction operator +(Fraction a) => a;
        //***

        #endregion

        //***

        #region get negative value

        //***
        public static Fraction operator -(Fraction a) => new Fraction(-a.Nominator, a.Denominator);
        //***

        #endregion

        //***

        #region sum overloading (+)

        //***
        public static Fraction operator +(Fraction a, Fraction b)
        {
            var fraction = new Fraction(a.Nominator * b.Denominator + b.Nominator * a.Denominator,
                a.Denominator * b.Denominator);
            return !fraction;
        }

        //***

        #endregion

        //***

        #region substraction overloading (-)

        //***
        public static Fraction operator -(Fraction a, Fraction b)
        {
            var fraction = a + (-b);
            return !fraction;
        }

        //***

        #endregion

        //***

        #region multiplication overloading (*)

        //***
        public static Fraction operator *(Fraction a, Fraction b)
        {
            var fraction = new Fraction(a.Nominator * b.Nominator,
                a.Denominator * b.Denominator);
            return !fraction;
        }

        //***

        #endregion

        //***

        #region division overloading (/)

        //***
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Nominator == 0)
                throw new DivideByZeroException();
            var fraction = new Fraction(a.Nominator * b.Denominator,
                a.Denominator * b.Nominator);
            return !fraction;
        }

        //***

        #endregion

        //***

        #region shorthand overloading : 'الإختزال'

        //***
        public static Fraction operator !(Fraction f) =>
            new Fraction(f.Nominator / pgcd(f.Nominator, f.Denominator),
                f.Denominator / pgcd(f.Nominator, f.Denominator));

        //***

        #endregion

        //***

        #region tostring overriding : 'إعادة وظيفة التنصيص'(+)

        //***
        public override string ToString() => $"{Nominator}/{Denominator}";
        //***

        #endregion

        //***

        #region equals : 'المساوات'
        //***
        public static bool operator ==(Fraction rf, Fraction lf)
        {
            if (rf is null)
            {
                if (lf is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lf.Equals(rf);
        }
        //***
        public static bool operator !=(Fraction lf, Fraction rf) => !(lf == rf);
        //***
        public static bool operator <(Fraction lf, Fraction rf) => lf.CompareTo(rf) < 0;
        //***
        public static bool operator >(Fraction lf, Fraction rf) => lf.CompareTo(rf) >= 0;
        //***
        public static bool operator >=(Fraction lf, Fraction rf) => lf.CompareTo(rf) > 0;
        //***
        public static bool operator <=(Fraction lf, Fraction rf) => lf.CompareTo(rf) <= 0;
        //***
        #endregion

        //***

        #endregion

        //***

        #region Interface implementation
        //***
        public bool Equals(Fraction f)
        {
            if (f is null) return false;
            return (Nominator * f.Denominator == Denominator * f.Nominator);
        }
        //***
        public int CompareTo(Fraction f)
        {
            try
            {
                var nb1 = Nominator * f.Denominator;
                var nb2 = f.Nominator * Denominator;

                if (nb2 > nb1)
                    return -1;
                return nb1 == nb2 ? 0 : 1;
            }
            catch (DivideByZeroException)
            {
                throw new ArgumentException($"error.");
            }
        }
        //***
        #endregion

        //***
    }
}
