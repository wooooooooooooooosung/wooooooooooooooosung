import time
from threading import Thread

def work(id, start, end, result):
    total = 0
    for i in range(start, end):
        print(i)
        total += i
    result.append(total)
    return

if __name__ == "__main__":

    start = time.time()


    result = list()
    th1 = Thread(target=work, args=(1, 0, 50000000, result))
    th2 = Thread(target=work, args=(2, 50000000, 100000000, result))
    
    th1.start()
    th2.start()
    th1.join()
    th2.join()

print(f"Result: { sum(result) }")
print(f"Time: { time.time() - start }")

print(len(result))