using System;
using System.Collections.Generic;

namespace RequestApprovalService.Silverspoon.Shamir
{
    public static class ShamirSecretSharing
    {
        public static List<Point> SplitSecret(int secret, int numberOfShares, int threshold)
        {
            Random rnd = new Random();
            List<int> kNumbers = new() { 0 };

            for (int i = 0; i < threshold - 1; i++)
            {
                int kShare = rnd.Next(1, 30);
                Console.WriteLine("K" + i + " = " + kShare);
                kNumbers.Add(kShare);
            }

            return GenerateShares(secret, numberOfShares, kNumbers);
        }

        private static List<Point> GenerateShares(int secret, int n, List<int> kNumbers)
        {
            List<Point> points = new();
            var equation = secret;
            for (int x = 1; x < n + 1; x++)
            {
                for (int i = 1; i < kNumbers.Count; i++)
                {
                    var kVal = kNumbers[i];
                    var xVal = Math.Pow(x, i);
                    equation += kVal * Convert.ToInt32(xVal);
                }

                Point point = new Point()
                {
                    X = x,
                    Y = equation
                };

                points.Add(point);
                equation = secret;
                Console.WriteLine("Share (Point) " + x + " : " + " (X: " + point.X + ", Y: " + point.Y + ")");
            }

            return points;
        }

        public static List<Point> GetRandomPoints(List<Point> points, int numOfPoints)
        {
            List<Point> pointsToReconstruct = new();
            Random rnd = new Random();
            while (pointsToReconstruct.Count < numOfPoints)
            {
                int index = rnd.Next(points.Count);
                if (points[index].Y != 0 && !pointsToReconstruct.Contains(points[index]))
                {
                    pointsToReconstruct.Add(points[index]);
                    Console.WriteLine("Share (Point) Added: " + points[index].X + " : " + points[index].Y);
                }
            }

            return pointsToReconstruct;
        }

        public static int ReconstructSecret(List<Point> points)
        {
            Decimal secret = 0;
            foreach (var point in points)
            {
                Decimal lagronge = 1;
                foreach (var otherPoint in points)
                {
                    Decimal divident = 1;
                    Decimal divisor = 1;
                    Decimal divisonResult = 1;
                    //int final = 1;
                    if (otherPoint != point)
                    {
                        divident *= (0 - otherPoint.X);
                        divisor *= (point.X - otherPoint.X);
                        divisonResult = divident / divisor;
                    }
                    lagronge *= divisonResult;
                }
                var pointResult = point.Y * lagronge;
                secret += pointResult;
            }

            int secretToInt = (int)Math.Round(secret);
            return secretToInt;
        }
    }
}
