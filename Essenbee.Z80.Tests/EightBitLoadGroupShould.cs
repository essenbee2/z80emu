﻿using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Essenbee.Z80.Tests
{
    public class EightBitLoadGroupShould
    {
        [Fact]
        public void LoadAfromBwhenOperationIsLDBA()
        {
            var fakeBus = A.Fake<IBus>();

            // Reading RAM will fetch opcode 0x47, which is LD B,A
            A.CallTo(() => fakeBus.Read(A<ushort>.Ignored, A<bool>.Ignored)).Returns<byte>(0x47);

            var cpu = new Z80() { A = 0x0F, B = 0x00, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x0F, cpu.B);
            Assert.Equal(cpu.A, cpu.B);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadBfromAwhenOperationIsLDAB()
        {
            var fakeBus = A.Fake<IBus>();

            // Reading RAM will fetch opcode 0x78, which is LD A,B
            A.CallTo(() => fakeBus.Read(A<ushort>.Ignored, A<bool>.Ignored)).Returns<byte>(0x78);

            var cpu = new Z80() { A = 0x00, B = 0x0F, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x0F, cpu.A);
            Assert.Equal(cpu.B, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void NoOpwhenOperationIsLDAA()
        {
            var fakeBus = A.Fake<IBus>();

            // Reading RAM will fetch opcode 0x7F, which is LD A,A
            A.CallTo(() => fakeBus.Read(A<ushort>.Ignored, A<bool>.Ignored)).Returns<byte>(0x7F);

            var cpu = new Z80() { A = 0x03, B = 0x0F, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x03, cpu.A);
            Assert.Equal(0x0F, cpu.B);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadAwithValueWhenOperationIsLDAn()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new byte[]
            {
                0x3E,
                0x02,
            };

            // Reading RAM will fetch opcode 0x3E, which is LD A,n,
            // and then the operand n (2 in this case)...
            A.CallTo(() => fakeBus.Read(A<ushort>.Ignored, A<bool>.Ignored)).ReturnsNextFromSequence(program);

            // Load 2 into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x02, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadHwithValueWhenOperationIsLDHn()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new byte[]
            {
                0x26,
                0x1E,
            };

            // Reading RAM will fetch opcode 0x26, which is LD H,n,
            // and then the operand n...
            A.CallTo(() => fakeBus.Read(A<ushort>.Ignored, A<bool>.Ignored)).ReturnsNextFromSequence(program);

            // Load 1E into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x1E, cpu.H);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadAwithValueAtMemoryLocationinHLwhenOperationIsLDAHL()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0x7E },
                { 0x0081, 0x02 },
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FF, 0x08 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
            };

            // Reading RAM will fetch opcode 0x7E, which is LD A, (HL);
            // the operand 0x08 is stored in the location pointed to by HL (0x08FF)...

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            // Load 2 into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, H= 0x08, L = 0xFF, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x08, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadAwithValueAtMemoryLocationinIXpludDwhenOperationIsLDRIXD_GivenDisZero()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xDD },
                { 0x0081, 0x7E },
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x15 },
                { 0x08FF, 0x08 }, // IX
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x05 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            // Load 2 into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, IX = 0x08FF, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x08, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadAwithValueAtMemoryLocationinIXpludDwhenOperationIsLDRIXD_GivenDisPositive()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xDD },
                { 0x0081, 0x7E },
                { 0x0082, 0x03 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x15 },
                { 0x08FF, 0x08 }, // IX
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x05 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            // Load 2 into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, IX = 0x08FF, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x05, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }

        [Fact]
        public void LoadAwithValueAtMemoryLocationinIXpludDwhenOperationIsLDRIXD_GivenDisNegative()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xDD },
                { 0x0081, 0x7E },
                { 0x0082, 0xFF },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x15 },
                { 0x08FF, 0x08 }, // IX
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x05 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            // Load 2 into register A, which starts out having a value of 3...
            var cpu = new Z80() { A = 0x03, IX = 0x08FF, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x15, cpu.A);

            // No affect on Condition Flags
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
        }
    }
}
