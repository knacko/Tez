using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdjacencyMatrix {
	
	int[,] m;
	int size;
		
	public AdjacencyMatrix(List<TessellationPolygon> polygons) {
		
		m = new int[polygons.Count,polygons.Count];
		
		foreach (TessellationPolygon p in polygons) {
			
			setElement (p.getID(), p.getID(), p.getState()); 
					
			foreach (TessellationPolygon subP in p.getAdjacentPolygons()) {
				
				setElement(subP.getID(), p.getID(), subP.getState());	
				
			}
			
		}	
		
		size = polygons.Count;
		
	}

	private void setElement(int x, int y, bool state) {
		
		m[y,x] = state ? 1 : -1;
		
	}
	
	private void flipRow(int row) {
		
		for(int i = 0; i < size; i++) {
			
			m[row,i] *= -1;
			
		}
		
	}
		
	private void flipColumn(int col) {
		
		for(int i = 0; i < size; i++) {
			
			m[i,col] *= -1;
			
		}
		
	}
	
	private void flipElement(int id) {
		
		m[id,id] *= -1;
		
	}
	
	private void flipPolygon(int id) {
		
		flipRow(id);
		flipColumn(id);
		flipElement(id); //because element is flipped twice
			
	}
	
	public override string ToString() {
		
		string s = "";
		
		for (int y = 0; y < size; y++) {
			
			s += "[";
			
			for (int x = 0; x < size; x++) {
				
				s += (m[y,x] != -1 ? " " : "") + m[y,x] + " ";
				
			}
			
			s = s.Remove(s.Length - 1);
			
			s += "]\n";
		}
		
		return s;
	}
}
