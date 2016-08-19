using System.Reflection;
using System.Runtime.Versioning;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if COM_INTEROP
[assembly: AssemblyTitle("ACBr.Net Core ActiveX")]
[assembly: AssemblyDescription("ACBr.Net Core ActiveX")]
[assembly: AssemblyProduct("ACBr.Net Core ActiveX")]
[assembly: TypeLibVersion(109, 23)]
#else
[assembly: AssemblyTitle("ACBr.Net Core")]
[assembly: AssemblyDescription("ACBr.Net Core")]
[assembly: AssemblyProduct("ACBr.Net Core")]
#endif

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ACBr.Net")]
[assembly: AssemblyCopyright("Copyright © Grupo ACBr.Net 2014 - 2016")]
[assembly: AssemblyTrademark("Grupo ACBr.Net https://acbrnet.github.io")]
[assembly: AssemblyKeyFile(@"../../../acbr.net.snk")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
#if COM_INTEROP
[assembly: ComVisible(true)]
#else
[assembly: ComVisible(false)]
#endif

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("72CEC839-4E63-4AEA-A2B4-370ED6F44B1A")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.1.1")]

//Internal Visible
//Especificar as assembly que tem acesso aos metodos internos da dll
[assembly: InternalsVisibleTo("ACBr.Net.Boleto, PublicKey=0024000004800000940000000602000000240000525341310004000001000100f74260d05a81ed3f35217680435c5b5e65dadf01ca0b54eae8a55ec6e120b40e45bd98f668ec1894f47bd93e7c7bc8dcfbc9c6f443507cce8092d59325ba403961936eb3d0a36d1171f49c605d185a80f4782525a957a3c509bbc369afa230330b74f7858f91dbd84a16389ea7fa602b4245203361e37d0b2e437fa5621762d7")]