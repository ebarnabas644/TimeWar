namespace TimeWar.LogicTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TimeWar.Logic.Interfaces;

    /// <summary>
    /// Test class for logic methods.
    /// </summary>
    public class Tests
    {
        private Mock<ICommand> mockedCommand;
        private Mock<ICommandManager> mockedCommandManager;

        /// <summary>
        /// Sets up testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mockedCommand = new Mock<ICommand>(MockBehavior.Loose);
            this.mockedCommandManager = new Mock<ICommandManager>(MockBehavior.Loose);
        }
    }
}
