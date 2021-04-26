using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize = 30000)
		{
            if (Instructions == null)
                Instructions = program;
            else Instructions += program;
            Memory = new byte[memorySize];
            MemoryPointer = 0;
            InstructionPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
            if (InstructionList == null)
                InstructionList = new Dictionary<char, Action<IVirtualMachine>> { [symbol] = execute };
            else InstructionList.Add(symbol, execute);
        }
        public Dictionary<char, Action<IVirtualMachine>> InstructionList;
        public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public void Run()
		{
            if (InstructionList != null)
            {
                while (InstructionPointer < Instructions.Length)
                {
                    if (InstructionList.ContainsKey(Instructions[InstructionPointer]))
                    {
                        InstructionList[Instructions[InstructionPointer]].Invoke(this);
                        InstructionPointer++;
                    }
                    else continue;
                }
            }
        }
	}
}