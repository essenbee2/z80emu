﻿using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Essenbee.Z80.Tests
{
    public class EightBitArithmeticLogicGroupShould
    {
        [Fact]
        public void LoadAWithThreeWhenAddingOneAndTwoForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x01, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x03, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWithZeroWhenAddingZeroAndZeroForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x00 }, // n = 0
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x00, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x00, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.True((cpu.F & Z80.Flags.Z) == Z80.Flags.Z); // Zero flag should be set
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith129WhenAdding127And2ForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x7F, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x81, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.True((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith2AndOverfowWhenAdding129And129ForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x81 }, // n = 129
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x81, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x02, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.True((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.True((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith32WhenAdding2And30ForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x1E, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x20, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.True((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith8WhenAdding4And4ForOpcodeADDAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xC6 }, //ADD A, n
                { 0x0081, 0x04 }, // n = 4
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x04, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x08, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.True((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWithThreeWhenAddingOneAndTwoForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x01, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x03, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWithZeroWhenAddingZeroAndZeroForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x00 }, // n = 0
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x00, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x00, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.True((cpu.F & Z80.Flags.Z) == Z80.Flags.Z); // Zero flag should be set
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith129WhenAdding127And2ForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x7F, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x81, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.True((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith2AndOverfowWhenAdding129And129ForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x81 }, // n = 129
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x81, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x02, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.True((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.True((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith32WhenAdding2And30ForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x02 }, // n = 2
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x1E, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x20, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.True((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.True((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.False((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith8WhenAdding4And4ForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x04 }, // n = 4
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x04, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x08, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.True((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }

        [Fact]
        public void LoadAWith9WhenAdding4And4AndCarryFlagSetForOpcodeADCAN()
        {
            var fakeBus = A.Fake<IBus>();

            var program = new Dictionary<ushort, byte>
            {
                // Program Code
                { 0x0080, 0xCE }, //ADC A, n
                { 0x0081, 0x04 }, // n = 4
                { 0x0082, 0x00 },
                { 0x0083, 0x00 },
                { 0x0084, 0x00 },

                // Data
                { 0x08FB, 0x00 },
                { 0x08FC, 0x00 },
                { 0x08FD, 0x00 },
                { 0x08FE, 0x00 },
                { 0x08FF, 0x00 },
                { 0x0900, 0x00 },
                { 0x0901, 0x00 },
                { 0x0902, 0x00 },
                { 0x0903, 0x00 },
                { 0x0904, 0x00 },
                { 0x0905, 0x00 },
                { 0x0906, 0x00 },
            };

            A.CallTo(() => fakeBus.Read(A<ushort>._, A<bool>._))
                .ReturnsLazily((ushort addr, bool ro) => program[addr]);

            var cpu = new Z80() { A = 0x04, F = Z80.Flags.C, PC = 0x0080 };
            cpu.ConnectToBus(fakeBus);
            cpu.Tick();

            Assert.Equal(0x09, cpu.A);
            Assert.False((cpu.F & Z80.Flags.N) == Z80.Flags.N);
            Assert.False((cpu.F & Z80.Flags.Z) == Z80.Flags.Z);
            Assert.False((cpu.F & Z80.Flags.S) == Z80.Flags.S);
            Assert.False((cpu.F & Z80.Flags.H) == Z80.Flags.H);
            Assert.False((cpu.F & Z80.Flags.P) == Z80.Flags.P);
            Assert.False((cpu.F & Z80.Flags.C) == Z80.Flags.C);
            Assert.False((cpu.F & Z80.Flags.U) == Z80.Flags.U);
            Assert.True((cpu.F & Z80.Flags.X) == Z80.Flags.X);
        }
    }
}
