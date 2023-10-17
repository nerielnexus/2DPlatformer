< GJJ_PortalMaster, GJJ_Portal 메뉴얼 >

1. GJJ_PortalMaster
    - Portal CoolDown Time
        - 포탈의 재사용 대기 시간을 설정한다.
        - 이 만큼의 시간이 지난 이후에만 포탈을 다시 이용할 수 있다.

2. GJJ_Portal
    - Portal Current Stauts
        - 포탈이 이용 가능한 상태인지를 설정한다.
        - OPEN
            - 열려있는 이미지로 표현된다.
            - 출구와 입구로 사용된다.
            - 반대쪽 포탈이 CLOSE 라면 반대쪽으로 텔레포트 할 수 없다.
        - CLOSE
            - 닫혀있는 이미지로 표현된다.
            - 반대쪽 포탈이 OPEN 인 경우에만, 반대쪽으로만 텔레포트 할 수 있다.
        - NONE
            - 문이 없는 이미지로 표현된다.
            - 포탈이 비활성화 되었음을 표현한다.
    - Portal Texture
        - OPEN, CLOSE, NONE 상태를 표현할 이미지를 설정한다.
    - 포탈 사용 방법
        - (22.12.27) 작성일 기준 플레이어 캐릭터가 포탈에 겹쳐선 후 왼쪽 ALT 를 누르면 이동한다.