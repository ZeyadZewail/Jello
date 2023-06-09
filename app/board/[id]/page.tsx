"use client";

import usePocketBase from "@/helpers/usePocketBase";
import Board from "@/types/Board";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const BoardPage = ({
	params,
	searchParams,
}: {
	params: { id: string };
	searchParams: { [key: string]: string | string[] | undefined };
}) => {
	const { pb, authStore } = usePocketBase();
	const [board, setBoard] = useState<Board>();
	const [columns, setColumns] = useState<Column[]>([]);
	const [loading, setLoading] = useState(true);
	const { push } = useRouter();

	useEffect(() => {
		if (authStore?.model?.id) {
			const getBoards = async () => {
				const filterString = `id = "${params.id}"`;

				const resultList = await pb.collection("boards").getFirstListItem<Board>("");

				setBoard(resultList);
				setLoading(false);
			};

			getBoards();
		}
	}, [authStore]);

	return <div>boardpage {params.id}</div>;
};

export default BoardPage;
