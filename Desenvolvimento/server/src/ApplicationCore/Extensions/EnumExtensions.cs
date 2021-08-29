using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace ApplicationCore.Extensions
{
    public static class EnumExtensions<T>
    {
        public static bool IsDefined(string name)
        {
            return Enum.IsDefined(typeof(T), name);
        }

        public static bool IsDefined(T value)
        {
            return Enum.IsDefined(typeof(T), value);
        }

        public static IEnumerable<T> GetValues()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public static class EnumExtensions
    {
        const string valorDefault = "Sem Descrição";
        /// <summary>
        /// Devolve a descrição do enum selecionado
        /// </summary>
        /// <param name="enumerador"></param>
        /// <returns>Descrição</returns>
        public static string Descricao(this Enum enumerador)
        {

            if (enumerador.IsNull())
                return valorDefault;

            var fieldInfo = enumerador.GetType().GetField(enumerador.ToString());
            var atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return atributos.Length > 0 ? atributos[0].Description ?? valorDefault : enumerador.ToString();
        }

        /// <summary>
        /// Devolve a descrição do enum selecionado
        /// </summary>
        /// <param name="enumerador"></param>
        /// <param name="descricaoPadrao">Descrição a ser devolvida, caso não encontre uma descrição</param>
        /// <returns>Descrição</returns>
        public static string Descricao(this Enum enumerador, string descricaoPadrao)
        {
            if (enumerador.IsNull())
                return descricaoPadrao;

            var fieldInfo = enumerador.GetType().GetField(enumerador.ToString());
            var atributos = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return atributos.Length > 0 ? atributos[0].Description ?? descricaoPadrao : enumerador.ToString();
        }

        /// <summary>
        /// Devolve a descrição do enum (Padrão XmlAtribute)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ValorDefinidoXmlAttribute(this Enum value)
        {
            var campo = value.GetType().GetField(value.ToString());
            var valores = (XmlEnumAttribute[])campo.GetCustomAttributes(typeof(XmlEnumAttribute), false);

            if (valores.Length > 0)
                return valores[0].Name.ToString();

            return value.ToString();
        }

        /// <summary>
        /// Devolve o enum de um determinado tipo que possua XmlEnumAttribute informado
        /// </summary>
        /// <typeparam name="T">Tipo do Enum que deverá ser pesquisado</typeparam>
        /// <param name="value">Valor do XmlEnumAttribute</param>
        /// <returns>Retorna o enum encontrado. Caso contrário devolve nulo</returns>
        public static object PesquisarEnumPorXmlAttribute<T>(string value)
        {
            foreach (object obj in Enum.GetValues(typeof(T)))
            {
                if (((Enum)obj).ValorDefinidoXmlAttribute().Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (T)obj;
            }

            return null;
        }

        /// <summary>
        /// Verifica se o objeto está nulo
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static bool IsNull(this object @object)
        {
            return @object == null;
        }

        /// <summary>
        /// Converte um valor de um Enum para outro Enum
        /// </summary>
        /// <typeparam name="M">Enum de saída</typeparam>
        /// <param name="mensagem">Mensagem padronizada em caso de não se encontrar um valor equivalente</param>
        /// <returns>Valor do Enum de saída</returns>
        public static M ParseTo<M>(this Enum valor, string mensagem = "")
        {

            if (String.IsNullOrEmpty(mensagem))
                mensagem = String.Format("Erro: informação: \"{0}\" é inválido", valor.ToString());

            try
            {
                var entrada = Enum.GetName(GetNonNullableModelType(valor.GetType()), valor);
                var saida = GetNonNullableModelType(typeof(M));

                return (M)Enum.Parse(saida, entrada, true);
            }
            catch
            {
                throw new ApplicationException(mensagem);
            }
        }

        public static M ParseTo<M>(this Enum valor, M padrao)
        {
            if (valor == null)
                return padrao;

            try
            {
                return valor.ParseTo<M>();
            }
            catch
            {
                return padrao;
            }
        }

        private static Type GetNonNullableModelType(Type propertyType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(propertyType);
            if (underlyingType != null)
            {
                propertyType = underlyingType;
            }
            return propertyType;
        }

        /// <summary>
        /// Retornar o valor do enumerador definido na classe como XmlEnum
        /// Ex.:  [XmlEnum("1001")]
        ///       Item1001,
        /// </summary>
        public static String ToStringXmlValue(this Enum nType)
        {
            Type oSystype = nType.GetType();
            string strName = System.Enum.GetName(oSystype, nType);
            FieldInfo oFieldInfo = oSystype.GetField(strName);
            object[] rgObjs = oFieldInfo.GetCustomAttributes(typeof(XmlEnumAttribute), false);
            foreach (object obj in rgObjs)
            {
                XmlEnumAttribute oDesc = obj as XmlEnumAttribute;
                if (oDesc != null)
                {
                    return oDesc.Name;
                }
            }

            return "0";
        }
    }
}
