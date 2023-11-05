import win32serviceutil
import servicemanager
import win32service
import win32event
import time

class LogTestService(win32serviceutil.ServiceFramework):
    _svc_name_ = "LogTestService"
    _svc_display_name_ = "Log Test Service"
    _svc_description_ = "Advanced Programming Engaged Learning"

    log_file = open('C:/Users/sung/Desktop/고급플밍/ServiceLog.txt', 'a')
    log_count = 1

    def write_log(self, text):
        self.log_file.write("[ " + time.ctime() + " ] " + text + "\n")
        self.log_file.flush()
    
    def __init__(self, args):

        # 서비스 시작 명령
        win32serviceutil.ServiceFramework.__init__(self, args)
        self.hWaitStop = win32event.CreateEvent(None, 0, 0, None)

    def SvcStop(self):

        self.write_log("구동 중지")

        # 서비스 정지 명
        self.ReportServiceStatus(win32service.SERVICE_STOP_PENDING)
        win32event.SetEvent(self.hWaitStop)

    def SvcDoRun(self):

        self.write_log("구동 시작")

        # 동작할 함수 호출
        self.main()

    def main(self):
        
        while True:

            # 1초마다 로그 남기기
            self.write_log("구동중 " + str(self.log_count) + "번 째 로그")
            self.log_count = self.log_count + 1
            time.sleep(1)
            
            # 서비스 중지 이벤트를 확인
            if win32event.WaitForSingleObject(self.hWaitStop, 0) == win32event.WAIT_OBJECT_0:
                break

        log_file.close()  # 로그 파일 닫기

# 시작점
if __name__ == '__main__':
    win32serviceutil.HandleCommandLine(LogTestService)
