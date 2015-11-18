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
 * Title: FANN C# Accessor
 */
using FannWrapperFloat;
namespace FANNCSharp.Float
{
    /* Class: Accessor
       
       Provides fast access to an array of floats
    */
    public class Accessor : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal Accessor(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Accessor obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        ~Accessor()
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
                        fannfloatPINVOKE.delete_Accessor(swigCPtr);
                    }
                    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
                }
                global::System.GC.SuppressFinalize(this);
            }
        }
        /* Method: Get
           Parameters:
                      index - The index of the element to return
   
            Return:
                 A float at index
        */
        public float Get(int index)
        {
            float ret = fannfloatPINVOKE.Accessor_Get(swigCPtr, index);
            return ret;
        }

        internal static Accessor FromPointer(SWIGTYPE_p_float t)
        {
            global::System.IntPtr cPtr = fannfloatPINVOKE.Accessor_FromPointer(SWIGTYPE_p_float.getCPtr(t));
            Accessor ret = (cPtr == global::System.IntPtr.Zero) ? null : new Accessor(cPtr, false);
            return ret;
        }
    }
}
