﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softbuild.Media.Effects
{
    public class PosterizeEffect : IEffect
    {
        /// <summary>
        /// 調整するポスタライズレベル
        /// </summary>
        private byte Level { get; set; }

        /// <summary>
        /// PosterizeEffect クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="level">ポスタライズレベルの値(0～255 標準:1)</param>
        public PosterizeEffect(byte level)
        {
            Level = level;
        }

        /// <summary>
        /// ポスタライズ処理をおこなう
        /// </summary>
        /// <param name="width">ビットマップの幅</param>
        /// <param name="height">ビットマップの高さ</param>
        /// <param name="source">処理前のピクセルデータ</param>
        /// <returns>処理後のピクセルデータ</returns>
        public byte[] Effect(int width, int height, byte[] source)
        {
            // ピクセルデータの数を計算する
            int pixelCount = width * height;

            // 処理後のピクセルデータを格納するためのバッファを生成する
            var dest = new byte[source.Length];

            // ゼロ割を防ぐためLevelが0だった場合、1に繰り上げる
            var level = (double)((Level == 0) ? 1 : Level);
            var step = (level / 255 * 100);

            for (int i = 0; i < pixelCount; i++)
            {
                var index = i * 4;

                // 処理前のピクセルから各BGAR要素を取得する
                double b = source[index + 0];
                double g = source[index + 1];
                double r = source[index + 2];
                var a = source[index + 3];

                var db = Math.Round(b / step) * step;
                var dg = Math.Round(g / step) * step;
                var dr = Math.Round(r / step) * step;

                // 処理後のピクセルデータを出力用バッファへ格納する
                dest[index + 0] = (byte)Math.Min(255, Math.Max(0, db));
                dest[index + 1] = (byte)Math.Min(255, Math.Max(0, dg));
                dest[index + 2] = (byte)Math.Min(255, Math.Max(0, dr));
                dest[index + 3] = a;
            }

            return dest;
        }
    }
}
