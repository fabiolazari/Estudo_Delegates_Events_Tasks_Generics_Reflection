using Estudo_Delegates_events.Entidades;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Estudo_Delegates_events.Servicos
{
	public class GenericaService
	{

        public object MyObject { get; set; }
		public Generica Generica { get; set; }
		public string NomeClasse { get; set; }
		public Dictionary<string, Type> Fields { get; set; }

        public void CreateNewObject()
        {
            var myType = CompileResultType();
            Generica = (Generica)Activator.CreateInstance(myType);
            //MyObject = Activator.CreateInstance(myType);
        }

        public Type CompileResultType()
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (KeyValuePair<string, Type> field in Fields)
                CreateProperty(tb, field.Key, field.Value);

            Type objectType = tb.CreateType();
            return objectType;
        }

        private TypeBuilder GetTypeBuilder()
        {
            string typeSignature = typeof(Generica).Name;
            AssemblyName an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule"); //(aName.Name + ".dll");

                // define tipo "ClasseDinamica" herdando de "ClassePai"
                //Type tipoClassePai = Type.GetType("ClassePai");

            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                                                      TypeAttributes.Public |
                                                      TypeAttributes.Class |
                                                      TypeAttributes.AutoClass |
                                                      TypeAttributes.AnsiClass |
                                                      TypeAttributes.BeforeFieldInit |
                                                      TypeAttributes.AutoLayout,
                                                      typeof(Generica));
            return tb;
        }

        private void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | 
                                                                                   MethodAttributes.SpecialName | 
                                                                                   MethodAttributes.HideBySig, 
                                                                                   propertyType, 
                                                                                   Type.EmptyTypes);

            MethodBuilder setPropMthdBldr = tb.DefineMethod("set_" + propertyName, MethodAttributes.Public |
                                                                                   MethodAttributes.SpecialName |
                                                                                   MethodAttributes.HideBySig,
                                                                                   null, 
                                                                                   new[] { propertyType });

            ILGenerator getIl = getPropMthdBldr.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }

        public List<Generica> Add(Generica generica)
        {
            List<Generica> lGenerica = new List<Generica>();

            lGenerica.Add(generica);


            /*Type tipo = classe.GetType();

            PropertyInfo[] propriedades = classe.GetType().GetProperties();

            foreach (var prop in propriedades)
            {
                PropertyInfo propertyInfo = generica.GetType().GetProperty(prop.Name); 
                PropertyInfo propertyInfo2 = classe.GetType().GetProperty(prop.Name);

                if (propertyInfo2 != null)
                {
                    propertyInfo.SetValue(generica, prop.GetValue(classe));
                }
            }
            */
            /*
            clienteDinamico.GetType().GetProperty("Id").SetValue(clienteDinamico, 1);
            clienteDinamico.GetType().GetProperty("Nome").SetValue(clienteDinamico, "Fabio Lazari");
            clienteDinamico.GetType().GetProperty("Email").SetValue(clienteDinamico, "fabio@gmail.com");
            clienteDinamico.GetType().GetProperty("Idade").SetValue(clienteDinamico, 46);

            clienteDinamico.GetType().GetProperty("Id").SetValue(clienteDinamico, 1);
            clienteDinamico.GetType().GetProperty("Nome").SetValue(clienteDinamico, "Fabio Lazari");
            clienteDinamico.GetType().GetProperty("Email").SetValue(clienteDinamico, "fabio@gmail.com");
            clienteDinamico.GetType().GetProperty("Idade").SetValue(clienteDinamico, 46);
            */

            return lGenerica;
        }
    }
}
