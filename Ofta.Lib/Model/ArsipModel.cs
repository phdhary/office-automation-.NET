using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.Model
{
    public interface IArsipKey
    {
        string ArsipID { get; set; }     
    }

    public interface IArsipTglJam
    {
        DateTime TglJamArsip { get; set; }
    }

    public interface IArsipEntity:
        IArsipKey, IArsipTglJam, IPegKey, IJenisArsipKey
    {

    }
    public class ArsipModel
    {
    }
}
