//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PPPKBrunoHrgovicMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActorUploadedFiles
    {
        public int IDActorUploadedFiles { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int ActorIDActor { get; set; }
    
        public virtual Actor Actor { get; set; }
    }
}
