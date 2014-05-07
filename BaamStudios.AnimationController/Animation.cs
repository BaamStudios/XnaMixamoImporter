using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Microsoft.Xna.Framework;
using SkinnedModel;

namespace BaamStudios.AnimationController
{
    public class Animation
    {
        public readonly string Name;
        public readonly AnimationPlayer AnimationPlayer;
        public readonly SkinningData SkinningData;

        public Animation(string name, Stream file)
        {
            Name = name;
            SkinningData = LoadSkinningData(file);
            AnimationPlayer = new AnimationPlayer(SkinningData);
        }

        public static SkinningData LoadSkinningData(Stream file)
        {
            return (SkinningData) new BinaryFormatter().Deserialize(file);

            // old json importer. changed to binary because json files are twice the size.

            //JsConfig<Matrix>.DeSerializeFn = r =>
            //{
            //    if (string.IsNullOrEmpty(r) || r.Length < 3)
            //        return Matrix.Identity;

            //    var values = r.Substring(1, r.Length - 2);
            //    var split = values.Split(',');
            //    if (split.Length != 16)
            //        return Matrix.Identity;

            //    var result = new Matrix();
            //    var i = 0;
            //    result.M11 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M12 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M13 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M14 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M21 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M22 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M23 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M24 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M31 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M32 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M33 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M34 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M41 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M42 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M43 = float.Parse(split[i++], CultureInfo.InvariantCulture);
            //    result.M44 = float.Parse(split[i++], CultureInfo.InvariantCulture);

            //    return result;
            //};
            //var serializer = new JsonSerializer<SkinningData>();
            //return serializer.DeserializeFromString(new StreamReader(file).ReadToEnd());
        }

        public void Start()
        {
            AnimationPlayer.StartClip(SkinningData.AnimationClips.First().Value);
            AnimationPlayer.Update(TimeSpan.Zero, true, Matrix.Identity);
        }
    }
}
