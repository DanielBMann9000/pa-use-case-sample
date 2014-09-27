/***************************************************************************************
* 
* Copyright 2014 PreEmptive Solutions, LLC.
* 
* You may not use this file except in compliance with the PreEmptive Solutions License.
* You may obtain a copy of the License at http://www.preemptive.com/eula.
* This software is distributed on an "AS IS" basis.
* 
****************************************************************************************/

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyProduct("PreEmptive Analytics Workbench")]
[assembly: AssemblyCompany("PreEmptive Solutions, LLC")]
[assembly: AssemblyCopyright("Copyright © 2014 PreEmptive Solutions, LLC")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

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
[assembly: AssemblyVersion("1.1.0")]
