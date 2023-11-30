
export function addScrollMove() {
   

    // 鼠标模拟问题
    let father = document.querySelector('.moveparent');
    // 判断鼠标是否按下
    let isMouseDown = false;
    // 记录Y轴滚动距离
    let scrollTop = 0;
    // 记录X轴滚动距离
    let scrollLeft = 0;
    // 记录鼠标落点X坐标
    let startX = 0;
    // 记录鼠标落点Y坐标
    let startY = 0;


    // 横向可移动的最大距离
    let limitX = 0;
    // 纵向可移动的最大距离
    let limitY = 0;
    
    if (father) {
        father.addEventListener('mousedown', e => {

            if (!father.scrollWidth) {
                return;
            }
            limitX = father.scrollWidth - father.offsetWidth;

            if (father.scrollHeight) {
                limitY = father.scrollHeight - father.offsetHeight;
            }
            // 修改按下状态
            isMouseDown = true;
            // 记录按下鼠标的位置, 用于计算鼠标的移动距离
            startX = e.offsetX;
            startY = e.offsetY;
        });
        father.addEventListener('mousemove', e => {
            // 判断鼠标移动时是否处于按下状态
            if (isMouseDown) {

                // 获取鼠标按下后移动的距离
                let offsetX = e.offsetX - startX;
                let offsetY = e.offsetY - startY;
                // PS: 需要注意的是当鼠标向上移动时, 滚动条应该向下移动, 所以这里都是减去的移动距离
                scrollTop = scrollTop - offsetY;
                scrollLeft = scrollLeft - offsetX;

                // TODO
                if (scrollTop >= limitY) {
                    // 当滑块移动到底端时
                    scrollTop = limitY;
                } else if (scrollTop <= 0) {
                    // 当滑块移动到顶端时
                    scrollTop = 0;
                }

                if (scrollLeft >= limitX) {
                    // 当滑块移动到左侧时
                    scrollLeft = limitX;
                } else if (scrollLeft <= 0) {
                    // 当滑块移动到右侧时
                    scrollLeft = 0;
                }
                // 将计算后的距离赋值给滚动条
                father.scrollTop = scrollTop;
                father.scrollLeft = scrollLeft;


            }
        });
        father.addEventListener('mouseup', () => {
            isMouseDown = false;
        });
    }
   
}
