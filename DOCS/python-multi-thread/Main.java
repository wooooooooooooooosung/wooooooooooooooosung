

public class Main {
	
	public static void main(String[] args) throws Exception {
		new Main();
	}
	
	public Main() {
		long startTime = System.currentTimeMillis();

		
//		TestThread th = new TestThread(0, 2000000000);
//		th.start();
//		while (th.isAlive()) {}
		
		TestThread th1 = new TestThread(0, 1000000000);
		TestThread th2 = new TestThread(1000000000, 2000000000);
		th1.start();
		th2.start();
		while (th1.isAlive() || th2.isAlive()) {}
		
		
		
		long time = System.currentTimeMillis() - startTime;
		System.out.println((double) time / 1000);
	}

	
	
	
	
	
	
	
	class TestThread extends Thread {

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
				sum += i;
			}
			System.out.println(sum);
			
		}
	}

}
