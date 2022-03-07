using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obi
{
	/**
	 * Sample script that colors fluid particles based on their vorticity (2D only)
	 */
	[RequireComponent(typeof(ObiEmitter))]
	public class RockDropEnergyColor : MonoBehaviour
	{
		ObiEmitter emitter;

		public Gradient grad;
		public Color defaultColor;
		public bool showEnergy = true;

		public float mass = 4.0f;
		public float parEnergy = 36.0f;

		void Awake()
		{
			emitter = GetComponent<ObiEmitter>();
		}

		public void OnEnable()
		{
		}



		private void calcualteOscilating()
		{

			if (showEnergy)
            {
				var m = mass;
				for (int i = 0; i < emitter.solverIndices.Length; i++)
				{

					var k = emitter.solverIndices[i];


					var velocity = emitter.solver.velocities[k].magnitude;

					float ke = (1 / 2.0f) * m * (velocity * velocity);
					var p = Mathf.Min(1.0f, ke / parEnergy);

					emitter.solver.colors[k] = grad.Evaluate(p);
				}
			}
			else
            {
				for (int i = 0; i < emitter.solverIndices.Length; i++)
				{

					var k = emitter.solverIndices[i];

					emitter.solver.colors[k] = defaultColor;
				}

			}
			
		}


		void LateUpdate()
		{
			if (!isActiveAndEnabled)
				return;
			calcualteOscilating();

		}

	}
}

