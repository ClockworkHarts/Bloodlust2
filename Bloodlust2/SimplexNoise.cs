using System.Collections;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

public class SimplexNoise1
{
    int Seed;

    public void setSeed(int s)
    {
        Seed = s;
    }

    public int getSeed()
    {
        return Seed;
    }

    public int randomSeed(int range, float x, float y)
    {
        float num = 0;
        num = x + y * 65536;
        int seed = Seed + (int)num;
        Random ran = new Random(seed);
        float rand = ran.Next(0, range);

        return (int)Math.Round(rand);
    }

    public float getPerlinNoise(int xx, int yy, int range, float chunkSize = 512)
    {
        float noise = 0;

        range = range/2;

        while (chunkSize > 0)
        {
            float index_x = (float)Math.Floor((xx / chunkSize));
            float index_y = (float)Math.Floor(yy / chunkSize);
            float t_x = (xx % chunkSize) / chunkSize;
            float t_y = (yy % chunkSize) / chunkSize;
            float r_00 = randomSeed(range, index_x, index_y);
            float r_01 = randomSeed(range, index_x, index_y + 1);
            float r_10 = randomSeed(range, index_x + 1, index_y);
            float r_11 = randomSeed(range, index_x + 1, index_y + 1);
            float r_0 = MathHelper.Lerp(r_00, r_01, t_y);
            float r_1 = MathHelper.Lerp(r_10, r_11, t_y);
            
            noise += MathHelper.Lerp(r_0, r_1, t_x);
            chunkSize = (float)Math.Floor(chunkSize / 2);
            range = (int)Math.Floor((range / 2f));
            range = Math.Max(1, range);
        }
        return noise;
    }
}
