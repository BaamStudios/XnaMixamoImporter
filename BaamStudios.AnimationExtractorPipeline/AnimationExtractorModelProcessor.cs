using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using SkinnedModel;
using SkinnedModelPipeline;

namespace BaamStudios.AnimationExtractorPipeline
{
    [ContentProcessor]
    public class AnimationExtractorModelProcessor : SkinnedModelProcessor
    {
        public override ModelContent Process(NodeContent input,
                                             ContentProcessorContext context)
        {
            var model = base.Process(input, context);

            var animFile = Path.ChangeExtension(context.OutputFilename, ".anim");
            SaveSkinningData((SkinningData)model.Tag, animFile);

            return model;
        }

        public static void SaveSkinningData(SkinningData skinningData, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, skinningData);
            }

            //JsConfig<Matrix>.SerializeFn = r =>
            //{
            //    var sw = new StringWriter(CultureInfo.InvariantCulture);
            //    sw.Write("[");
            //    sw.Write(r.M11);
            //    sw.Write(",");
            //    sw.Write(r.M12);
            //    sw.Write(",");
            //    sw.Write(r.M13);
            //    sw.Write(",");
            //    sw.Write(r.M14);
            //    sw.Write(",");
            //    sw.Write(r.M21);
            //    sw.Write(",");
            //    sw.Write(r.M22);
            //    sw.Write(",");
            //    sw.Write(r.M23);
            //    sw.Write(",");
            //    sw.Write(r.M24);
            //    sw.Write(",");
            //    sw.Write(r.M31);
            //    sw.Write(",");
            //    sw.Write(r.M32);
            //    sw.Write(",");
            //    sw.Write(r.M33);
            //    sw.Write(",");
            //    sw.Write(r.M34);
            //    sw.Write(",");
            //    sw.Write(r.M41);
            //    sw.Write(",");
            //    sw.Write(r.M42);
            //    sw.Write(",");
            //    sw.Write(r.M43);
            //    sw.Write(",");
            //    sw.Write(r.M44);
            //    sw.Write("]");
            //    return sw.ToString();
            //};
            //var serializer = new JsonSerializer<SkinningData>();
            //var json = serializer.SerializeToString(skinningData);
            //File.WriteAllText(path, json);
        }
    }
}
