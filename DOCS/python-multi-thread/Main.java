import java.util.ArrayList;

public class Main {
	
	public static void main(String[] args) throws Exception {
		new Main();
	}
	
	public Main() throws Exception {
		long startTime = System.currentTimeMillis();

		
		TestThread th = new TestThread(0, 100000000);
		th.start();
		th.join();
		
//		TestThread th1 = new TestThread(0, 50000000);
//		TestThread th2 = new TestThread(50000000, 100000000);
//		th1.start();
//		th2.start();
//		th1.join();
//		th2.join();
		
//		while(th.isAlive()) {}
		
		long time = System.currentTimeMillis() - startTime;
		System.out.println((double) time / 1000);
	}

	
	
	
	class TestThread extends Thread {

		private ArrayList<Integer> list = new ArrayList<Integer>();
		private int start;
		private int end;
		

		public TestThread(int start, int end) {
			this.start = start;
			this.end = end;
		}

		@Override
		public void run() {
			long sum = 0;
			for (int i = start; i < end; i++) {
				list.add(i);
			}
			
			for(int d : list) sum += d;
			System.out.println("sum: " + sum);
			
		}
	}

}
