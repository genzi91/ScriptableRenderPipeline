using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEditor.ShaderGraph
{
    class PropertyCollector
    {
        public struct TextureInfo
        {
            public string name;
            public int textureId;
            public bool modifiable;
        }

        private readonly List<IShaderValue> m_GraphInputs = new List<IShaderValue>();

        public void AddGraphInput(IShaderValue chunk)
        {
            if (m_GraphInputs.Any(x => x.shaderOutputName == chunk.shaderOutputName))
                return;
            m_GraphInputs.Add(chunk);
        }

        public string GetPropertiesBlock(int baseIndentLevel)
        {
            var sb = new StringBuilder();
            //foreach (var prop in m_GraphInputs) //.Where(x => x.generatePropertyBlock))
            //{
            //    for (var i = 0; i < baseIndentLevel; i++)
            //    {
            //        //sb.Append("\t");
            //        sb.Append("    "); // unity convention use space instead of tab...
            //    }
            //    sb.AppendLine(prop.GetPropertyBlockString());
            //}
            return sb.ToString();
        }

        public string GetPropertiesDeclaration(int baseIndentLevel)
        {
            var builder = new ShaderStringBuilder(baseIndentLevel);
            GetPropertiesDeclaration(builder);
            return builder.ToString();
        }

        public void GetPropertiesDeclaration(ShaderStringBuilder builder)
        {
            //builder.AppendLine("CBUFFER_START(UnityPerMaterial)");
            //foreach (var prop in m_GraphInputs.Where(n => n.isBatchable && n.generatePropertyBlock))
            //{
            //    builder.AppendLine(prop.GetPropertyDeclarationString());
            //}
            //builder.AppendLine("CBUFFER_END");
            //builder.AppendNewLine();

            foreach (var prop in m_GraphInputs)//.Where(n => !n.isBatchable || !n.generatePropertyBlock))
            {
                builder.AppendLine(prop.ToPropertyDefinition(AbstractMaterialNode.OutputPrecision.@float));
            }
        }

        public List<TextureInfo> GetConfiguredTexutres()
        {
            var result = new List<TextureInfo>();

            foreach (var prop in m_GraphInputs.Where(p => p.concreteValueType == ConcreteSlotValueType.Texture2D))
            {
                if (prop.shaderOutputName != null)
                {
                    var textureInfo = new TextureInfo
                    {
                        name = prop.shaderOutputName,
                        textureId = prop.value.textureValue != null ? prop.value.textureValue.GetInstanceID() : 0,
                        //modifiable = prop.modifiable
                    };
                    result.Add(textureInfo);
                }
            }

            foreach (var prop in m_GraphInputs.Where(p => p.concreteValueType == ConcreteSlotValueType.Texture2DArray))
            {
                if (prop.shaderOutputName != null)
                {
                    var textureInfo = new TextureInfo
                    {
                        name = prop.shaderOutputName,
                        textureId = prop.value.textureValue != null ? prop.value.textureValue.GetInstanceID() : 0,
                        //modifiable = prop.modifiable
                    };
                    result.Add(textureInfo);
                }
            }

            foreach (var prop in m_GraphInputs.Where(p => p.concreteValueType == ConcreteSlotValueType.Texture3D))
            {
                if (prop.shaderOutputName != null)
                {
                    var textureInfo = new TextureInfo
                    {
                        name = prop.shaderOutputName,
                        textureId = prop.value.textureValue != null ? prop.value.textureValue.GetInstanceID() : 0,
                        //modifiable = prop.modifiable
                    };
                    result.Add(textureInfo);
                }
            }

            foreach (var prop in m_GraphInputs.Where(p => p.concreteValueType == ConcreteSlotValueType.Cubemap))
            {
                if (prop.shaderOutputName != null)
                {
                    var textureInfo = new TextureInfo
                    {
                        name = prop.shaderOutputName,
                        textureId = prop.value.textureValue != null ? prop.value.textureValue.GetInstanceID() : 0,
                        //modifiable = prop.modifiable
                    };
                    result.Add(textureInfo);
                }
            }
            return result;
        }
    }
}
