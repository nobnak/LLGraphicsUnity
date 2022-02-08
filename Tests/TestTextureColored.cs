using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LLGraphicsUnity.Tests {

	public class TestTextureColored {

		[Test]
		public void TestTextureColoredSimplePasses() {

			var data = new GLProperty() {
				Color = Color.white
			};
			Assert.AreEqual(Color.white, data.Color);

			var gdata = new GLProperty(data) {
				Color = Color.green
			};
			Assert.AreEqual(Color.green, gdata.Color);
		}
	}
}
