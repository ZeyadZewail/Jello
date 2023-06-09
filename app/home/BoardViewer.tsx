"use client";

import { useEffect, useState } from "react";
import usePocketBase from "../../helpers/usePocketBase";
import Board from "../../types/Board";
import BoardOption from "@/components/boardOption/BoardOption";
import Spinner from "@/components/spinner/Spinner";
import { useRouter } from "next/navigation";
import LoadingWrapper from "@/hoc/LoadingWrapper";

const BoardViewer = () => {
	const { pb, authStore } = usePocketBase();
	const [boards, setBoards] = useState<Board[]>();
	const [loading, setLoading] = useState(true);
	const { push } = useRouter();

	useEffect(() => {
		if (authStore?.model?.id) {
			const getBoards = async () => {
				const filterString = `users ~ "${authStore?.model?.id}"`;

				const resultList = await pb.collection("boards").getFullList<Board>({
					filter: filterString,
					sort: "-created",
				});

				setBoards(resultList);
				setLoading(false);
			};

			getBoards();
		}
	}, [authStore]);

	const renderBoards = () => {
		if (!boards || boards.length == 0) {
			return <div>No Boards Yet.</div>;
		} else {
			return boards.map((board) => <BoardOption key={board.id} board={board} />);
		}
	};

	return (
		<LoadingWrapper loading={loading} spinnerSize={80}>
			<div className="flex min-h-screen w-full items-center flex-col md:gap-16 gap-8">
				<h1 className="md:text-8xl text-4xl	text-center font-bold mt-20">{`Hi ${authStore?.model?.username}`}</h1>
				<div
					className="grid gap-6 lg:w-3/5 md:h-4/6 w-full px-4 justify-center items-center"
					style={{ gridTemplateColumns: "repeat(auto-fit, 224px" }}>
					{renderBoards()}
				</div>
				<button
					className="bg-primary-button px-3 py-2 rounded-xl"
					onClick={() => {
						pb.authStore.clear();
						push("/login");
					}}>
					Log Out
				</button>
			</div>
		</LoadingWrapper>
	);
};

export default BoardViewer;
