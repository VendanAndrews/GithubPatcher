using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GithubPatcher
{
    class Loader
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, ARargs) =>
            {
                var resName = "GithubPatcher." + ARargs.Name.Split(new char[]{','})[0] + ".dll";
                var thisAssembly = Assembly.GetExecutingAssembly();
                using (var input = thisAssembly.GetManifestResourceStream(resName))
                {
                    if (input != null)
                    {
                        byte[] data = new byte[input.Length];
                        input.Read(data, 0, (int)input.Length);
                        return Assembly.Load(data);
                    }
                    return null;
                }
            };

            MainCore(args);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void MainCore(string[] args)
        {
            Program.Main(args);
        }
    }
}
