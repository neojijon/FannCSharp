//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.7
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------
/*
 * Title: FANN C# ArrayAccessor int
 */
using FannWrapperFixed;
using System.Collections.Generic;
using FANNCSharp;
using System.Collections;
namespace FANNCSharp.Fixed
{
    /* Class: ArrayAccessor
       
       Provides fast access to an array of array of ints
             
       Inherits from IAccessor when compiled with a target
       below .NET 4.5 (which is the default for the most
       compatibility). Inherits from <IReadOnlyList at https://msdn.microsoft.com/en-us/library/hh192385.aspx>
       when compiled with at or above .NET 4.5 to allow the
       user to take advantage of the multitude of extension
       methods on <IReadOnlyList at https://msdn.microsoft.com/en-us/library/hh192385.aspx>.
    */
#if NET45
    public class ArrayAccessor : IReadOnlyList<DataAccessor>, global::System.IDisposable
#else
    public class ArrayAccessor : IAccessor<DataAccessor>, global::System.IDisposable
#endif
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal ArrayAccessor(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ArrayAccessor obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        internal void SetLengths(int count, int length)
        {
            ArrayCount = count;
            ArrayLength = length;
        }

        ~ArrayAccessor()
        {
            Dispose();
        }
        /* Method: Dispose
        
            Destructs the accessor. Must be called manually.
        */
        public virtual void Dispose()
        {
            lock (this)
            {
                if (swigCPtr.Handle != global::System.IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        fannfixedPINVOKE.delete_IntArrayAccessor(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }
        /* Property: Item
           Parameters:
                      index - The index of the array to return
   
            Return:
                 A <DataAccessor> that provides fast access to an array
                 of doubles
        */
        public DataAccessor this[int index]
        {
            get
            {
                global::System.IntPtr cPtr = fannfixedPINVOKE.IntArrayAccessor_Get__SWIG_0(swigCPtr, index);
                DataAccessor ret = (cPtr == global::System.IntPtr.Zero) ? null : new DataAccessor(cPtr, false);
                ret.SetLength(ArrayCount);
                return ret;
            }
        }
        /* Method: Get
           Parameters:
                      x - The index of the array to access
                      y - The index in array x to return
   
            Return:
                 A int at position x,y in the array
        */
        public int Get(int x, int y)
        {
            int ret = fannfixedPINVOKE.IntArrayAccessor_Get__SWIG_1(swigCPtr, x, y);
            return ret;
        }
        /*  Method: GetEnumerator
            Returns an enumerator that can enumerate over the collection of DataAccessors
         */
        public IEnumerator<DataAccessor> GetEnumerator()
        {
            return (IEnumerator<DataAccessor>)new AccessorEnumerator<DataAccessor>(this);
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AccessorEnumerator<DataAccessor>(this);
        }

        internal static ArrayAccessor FromPointer(SWIGTYPE_p_p_int t)
        {
            global::System.IntPtr cPtr = fannfixedPINVOKE.IntArrayAccessor_FromPointer(SWIGTYPE_p_p_int.getCPtr(t));
            ArrayAccessor ret = (cPtr == global::System.IntPtr.Zero) ? null : new ArrayAccessor(cPtr, false);
            return ret;
        }

        /*  Property: Count
            The number of DataAccessors (arrays of ints) this object holds 
         */
        public int Count
        {
            get
            {
                return ArrayCount;
            }
        }
        internal int ArrayCount { get; set; }
        internal int ArrayLength { get; set; }
    }
}
