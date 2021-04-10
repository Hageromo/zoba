package mgodlewski;

import klesk.math.search.StateImpl;


	
	public class SudokuState extends StateImpl{


		
		public final int n;
		public final int n2;
		public byte [][] board;
		
		SudokuState(SudokuState aParent, int n){
			super(aParent);
			this.n=n;
			n2=n*n;
			board = new byte[n2][n2];
			
			if(aParent==null){
				for(int i=0;i<n2;i++){
					for(int j=0;j<n2;j++){
						board[i][j]=0;
					}
				}
					
			}
			else{
				for(int i=0;i<n2;i++){
					for(int j=0;j<n2;j++){
						board[i][j]=aParent.board[i][j];
					}
				}
			}
			computeHeuristicGrade();
			}
		public SudokuState(int n, String sudokuAsString){
			super(null);
			this.n=n;
			n2=n*n;
			board = new byte[n2][n2];
			if(n==3){				
				for(int i=0;i<n2;i++){
					for(int j=0;j<n2;j++){
						int index = i*n2+j;
						board[i][j] = Byte.valueOf(sudokuAsString.substring(index,index+1)).byteValue();
						
					}
					}
			}
			computeHeuristicGrade();
		}

		public SudokuState(SudokuState parent){
			super(parent);
			n=parent.n;
			n2=parent.n2;
			board= new byte[n2][n2];
			for(int i=0;i<n2;i++){
				for(int j=0;j<n2;j++){
					board[i][j]=parent.board[i][j];
				}
			}
			computeHeuristicGrade();                   
		}

		@Override
		public String toString() {
	
			String result="";
			
			for(int i=0;i<n2;i++){
				for(int j=0;j<n2;j++){
					
					result+=board[i][j];
					if(j<n2-1) 	result+=",";
				}
				if(i<n2-1)	result+="\n";
				}
			return result;
		}
		
		@Override
		public double computeHeuristicGrade() {
			int licznik=0;
			for(int i=0; i<n2; i++){
				for(int j=0;j<n2; j++){
					if(board[i][j]==0)
						licznik++;
				}
			}
			setH(licznik);
			return licznik;
		}
		
		private boolean isSequenceOk(byte [] temp){
			for(int i=0;i<n2;i++){
				if(temp[i]==0) continue;
				for(int j=i+1; j<n2;j++)
					if(temp[i]==temp[j])
						return false;
			}
			return true;
		}
		
		public boolean isAdmisible(){
			byte[]temp=new byte[n2];
			for(int i=0;i<n2;i++){
				for(int j=0;j<n2;j++){
					temp[j]=board[i][j];					
				}
				if(!isSequenceOk(temp))
					return false;
			}

			for(int i=0;i<n2;i++){
				for(int j=0;j<n2;j++){
					temp[j]=board[j][i];
				}
				if(!isSequenceOk(temp))
					return false;
			}
			
			for(int i =0;i<n2;i++){
				int j=i/n;
				int k=i%n;
				int z=0;
				for(int p=0;p<n;p++){
					for(int q=0;q<n;q++,z++)
						temp[z]=board[j*n+p][k*n+q];
				}
				if(!isSequenceOk(temp))
					return false;
					
			}
			
			return true;
		}
		@Override
		public String getHashCode() {
			
			return toString();
		}
		

	}


