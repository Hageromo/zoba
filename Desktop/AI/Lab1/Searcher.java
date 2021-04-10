package mgodlewski;

import java.util.ArrayList;
import java.util.List;

import klesk.math.search.AStarSearcher;
import klesk.math.search.State;

public class Searcher extends AStarSearcher{

	public Searcher(State aInitialState, boolean aIsStopAfterFirstSolution,
			boolean aIsStopAfterSecondSolution) {
		super(aInitialState, aIsStopAfterFirstSolution, aIsStopAfterSecondSolution);
		// TODO Auto-generated constructor stub
	}

	@Override
	public void buildChildren(State aParent) {
		List<State> children = new ArrayList<State>();
		SudokuState parent = (SudokuState) aParent;
		int i=0;
		int j=0;
		boolean temp = false;
		for(i=0;i<parent.n2;i++){
			for(j=0;j<parent.n2;j++){
				if(parent.board[i][j]==0){
					temp = true;
					break;
				}
			}
			if(temp) break;
				}
		
		for(int k=0;k<parent.n2;k++){
			SudokuState child = new SudokuState(parent);
			child.board[i][j] = (byte)(k+1);
			if(!child.isAdmisible()) 
				continue;
			child.computeHeuristicGrade();
			children.add(child);
		}
		parent.setChildren(children);
	}

	@Override
	public boolean isSolution(State aState) {
		SudokuState s=(SudokuState) aState;
		return ((s.isAdmisible())&&s.getH()==0);
		
	}

	public static void main(String[] args){
		
		int dl;
		String input="003020600900305001001806400008102900700000008006708200002609500800203009005010300";
		dl=input.length();
		dl=(int) Math.sqrt((double)Math.sqrt((double)dl));
		SudokuState s = new SudokuState(dl, input);
		System.out.println(s.isAdmisible());
		
		Searcher searcher = new Searcher(s, true, false);
		searcher.doSearch();
		System.out.println(searcher.getSolutions().get(0));
		
		
		
	}
}

