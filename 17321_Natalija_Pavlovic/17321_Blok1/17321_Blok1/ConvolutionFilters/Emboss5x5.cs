using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17321_Blok1
{
    class Emboss5x5 : ConvolutionFilterBase
    {
        public override string FilterName
        {
            get { return "EmbossFilter5x5"; }
        }

        private double factor = 1.0;
        public override double Factor
        {
            get { return factor; }
        }

        private double bias = 128.0;
        public override double Bias
        {
            get { return bias; }
        }

        private double[,] filterMatrix =
                new double[,] { {  -1,  -1,  0, 0, 0, },
                                 {  0, -1, 0,  0, 0, },
                                 {  0, 0,  0, 0, 0, },
                                 {  0,  0, 0,  1, 0, },
                                 {  0,  0, 0,  1, 1, }, };

        public override double[,] FilterMatrix
        {
            get { return filterMatrix; }
        }
    }
}
